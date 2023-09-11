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
  tasks: Task[] = [];
  sum: number = 0;
  filterText: string = '';
  filteredTasks: Task[] = [];
  filterTimeout: any;
  filterVisible: boolean = true;

  constructor(private taskService: TasksService, private router: Router) { }

  ngOnInit() {
    this.getAllTasks();
    this.applyFilter();
  }

  applyFilter() {
    clearTimeout(this.filterTimeout); // Clear any existing timeout
    this.filterTimeout = setTimeout(() => {
      console.log(this.filterText);

      const filterValue = this.filterText.toLowerCase();
      this.filteredTasks = this.tasks.filter((task) => {
        // Apply your desired filtering logic based on the survey properties
        return (
          task.nvType.toLowerCase().includes(filterValue) ||
          task.nvCategory.toLowerCase().includes(filterValue) ||
          task.nvFirstName.toLowerCase().includes(filterValue) ||
          task.nvStatus.toLowerCase().includes(filterValue) ||
          task.iTargetId.toString().includes(filterValue) ||
          task.dtEndDate.toString().includes(filterValue) ||
          task.nvOrigin.toLowerCase().includes(filterValue)
        );
      });
    }, 1000);
  }
  getAllTasks() {
    this.taskService.getTasks().subscribe((res: any) => {
      this.tasks = res;
      this.filteredTasks = res;
      console.log(this.tasks);
      this.sum = this.tasks.length;
    })
    console.log(this.tasks);

  }
  getStatusColor(iStatus: number): string {
    if (iStatus === 1)
      return '#00B4E6';
    else if (iStatus === 2)
      return '#ED4D9A';
    else if (iStatus === 3)
      return '#828282';
    else {
      return '#57BC83';

    }
  }
  toggleFilters() {
    this.filterVisible = !this.filterVisible;
  }
}



