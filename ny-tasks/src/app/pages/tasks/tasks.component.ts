import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TasksService } from 'src/app/services/tasks.service';
import { Task } from 'src/models/Task.model';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss']
})
export class TasksComponent {
  tasks: Task[]=[];
  sum: number = 0;
  filterText: string = '';
  filteredTasks: any;
  filterTimeout: any;
  filterVisible: boolean = true;

  constructor(private taskService: TasksService, private router: Router) { }

  ngOnInit() {
    this.getAllTasks();
    this.applyFilter();
  }

  applyFilter() {
    clearTimeout(this.filterTimeout); // Clear any existing timeout
    // this.filterTimeout = setTimeout(() => {
    //   console.log(this.filterText);

    //   const filterValue = this.filterText.toLowerCase();
    //   this.filteredTasks = this.surveys.filter((survey) => {
    //     // Apply your desired filtering logic based on the survey properties
    //     return (
    //       survey.nvSurveyName.toLowerCase().includes(filterValue) ||
    //       survey.dtEndSurveyDate.toString().includes(filterValue) ||
    //       survey.iRespondentsCount.toString().includes(filterValue) ||
    //       survey.iQuestionCount.toString().includes(filterValue) 
    //   );
    //   // });
    // }, 1000);
  }
  getAllTasks() {
    this.taskService.getTasks().subscribe((res: any) => {
      this.tasks = res;
      console.log(this.tasks);   
      this.sum = this.tasks.length;
    })
    console.log(this.tasks);

  }
  toggleFilters() {
    this.filterVisible = !this.filterVisible;
  }
}



