import { Component } from '@angular/core';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.scss']
})
export class TasksComponent {
tasks:any;
sum:number =0;
filterText: string = '';
filteredTasks:any;
filterTimeout: any;
filterVisible:boolean=true;

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
toggleFilters(){
  this.filterVisible = !this.filterVisible;
}
}



