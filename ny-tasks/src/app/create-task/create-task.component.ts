import { Component, Input, Output, EventEmitter } from '@angular/core';
import { SettingsService } from '../services/settings.service';
import { Target } from '@angular/compiler';
import { ChangeDetectorRef } from '@angular/core';
import { Observable, map, startWith } from 'rxjs';
import { Task } from 'src/models/Task.model';
import { FormGroup, FormControl, Validators, ValidatorFn, AbstractControl } from '@angular/forms';
import { TasksService } from '../services/tasks.service';
import { TargetsService } from '../services/targets.service';
import { NgZone } from '@angular/core';
@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.scss']
})
export class CreateTaskComponent {
  @Input()
  sidebarVisible: boolean = false;
  @Output()
  OutputVisible: EventEmitter<boolean> = new EventEmitter<boolean>();
  category: { label: string; value: string | null; }[] = [{ label: " ", value: null }, { label: "משימה חינוכית", value: "משימה חינוכית" }, { label: "משימה ניהולית", value: "משימה ניהולית" }];
  targetsTypesNames: { label: string; value: number; }[] = [];
  targetsNames: { label: string; value: number; }[] = [];
  tasksTypesNames: { label: string; value: number; }[] = [];
  students: { label: string; value: number; }[] = [];
  selectedStudent: { label: string; value: number; } = { label: "", value: 0 };
  isFormSubmitted = false;


  taskForm: FormGroup = new FormGroup({
    category: new FormControl("", [Validators.required]),
    student: new FormControl(null),
    typeTask: new FormControl(null, [Validators.required]),
    comments: new FormControl(null),
    target: new FormControl(null, [Validators.required]),
    endDate: new FormControl(null, [Validators.required, this.dateRangeValidator()]),
    // selectedCountry: new FormControl(null, [Validators.required]),
    // addressee: new FormControl("", [Validators.required]),
  });
  SendData() {
    this.taskForm.markAllAsTouched();
    if (this.taskForm.valid) {
      // this.sidebarVisible = false;
      let task: Task = this.makeTaskObject(this.taskForm);
      this._tasksService.addTask(task, this.taskForm.value.target).subscribe({
        next: (response: any) => {
          alert("success")
        },
        error: (error) => {

        },
      });

    }
    else {
       for (const controlName in this.taskForm.controls) {
        this.isInvalid(controlName);
      }
    }
  }

  dateRangeValidator(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const selectedDate = new Date(control.value);
      const currentDate = new Date();
      const threeMonthsLater = new Date();
      threeMonthsLater.setMonth(currentDate.getMonth() + 1);

      if (selectedDate >= currentDate && selectedDate <= threeMonthsLater) {
        return null;
      } else {
        return { dateOutOfRange: true };
      }
    };
  }

  isInvalid(controlName: string): boolean {
    const control = this.taskForm.get(controlName);
    return control?.touched && control?.invalid || false;
  }


  private makeTaskObject(taskForm: FormGroup): Task {
    var task = {} as Task;
    task.iTargetId = taskForm.value.target;
    task.iType = taskForm.value.typeTask;
    task.nvCategory = taskForm.value.category;
    task.dtEndDate = taskForm.value.endDate;
    task.iStatus = 1;
    task.nvOrigin = "עצמית";
    task.nvComments = taskForm.value.comments;
    task.nvStatus = "חדש";
    task.nvType = this.tasksTypesNames[taskForm.value.typeTask].label;
    if (taskForm.value.student == 0 ||taskForm.value.category=="ניהולית") {
      task.iStudentId = null;
    }
    else {
      task.iStudentId = taskForm.value.student?.value;
    }

    return task;
  }

  constructor(private _settingsService: SettingsService, private cdr: ChangeDetectorRef, private _tasksService: TasksService, private _targetsService: TargetsService, private ngZone: NgZone) { }

  ngOnInit(): void {
    this._settingsService.getTasksType().subscribe({
      next: (response: any[]) => {
        const emptyOption = { label: ' ', value: null };
        this.tasksTypesNames = [
          emptyOption,
          ...response.map((item) => ({
            label: item.nvType,
            value: item.iTypeId,
          }))];

      },
      error: (error) => {
        console.error('Error getting tasks type:', error);
      },
    });
    // let brunchNumber :number=Number(localStorage.getItem('selectedBranch'));
    let brunchNumber = 3;
    this._settingsService.getStudentForTask(brunchNumber).subscribe({
      next: (response: any[]) => {
        if (response && response.length > 0) {
          const emptyOption = { label: ' ', value: null };
          this.students = [
            emptyOption,
            ...response.map((item: { fullName: any; iPersonId: any; }) => ({
              label: item.fullName,
              value: item.iPersonId,
            }))
          ];

          ;
        }
      },
      error: (error) => {
        console.error('Error getting targets type:', error);
      }
    });

    this._settingsService.getTargetsType().subscribe({
      next: (response: any[]) => {
        if (response && response.length > 0) {
          const emptyOption = { label: ' ', value: null };
          this.targetsTypesNames = [
            emptyOption,
            ...response.map((item: { iTypeTargetId: any; nvTargetName: any; }) => ({
              label: item.nvTargetName,
              value: item.iTypeTargetId,
            }))];
          this._targetsService.getTargets().subscribe({
            next: (response: any[]) => {
              if (response && response.length > 0) {
                const emptyOption = { label: ' ', value: null };
                this.targetsNames = [
                  emptyOption,
                  ...response.map((item: { iTargetId: any; iTypeTargetId: any; }) => ({
                    label: this.targetsTypesNames[item.iTypeTargetId].label,
                    value: item.iTargetId,
                  }))];
              } else {
                console.error('Empty or unexpected response:', response);
              }

            },
            error: (error) => {
              console.error('Error getting targets type:', error);
            },

          });
        } else {
          console.error('Empty or unexpected response:', response);
        }
      },
      error: (error) => {
        console.error('Error getting targets type:', error);
      },
    });

    this.taskForm.get('category')?.valueChanges.subscribe(val => {
      if (val === "משימה חינוכית") {
        this.taskForm.controls['student'].setValidators([Validators.required]);
        this.taskForm.controls['student'].updateValueAndValidity();
      } else {
        this.taskForm.controls['student'].clearValidators();
        this.taskForm.controls['student'].updateValueAndValidity();
      }
    });
    

  }
}

