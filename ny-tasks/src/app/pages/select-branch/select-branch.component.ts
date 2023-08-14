import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CodeTable } from 'src/models/CodeTable.model';

@Component({
  selector: 'app-select-branch',
  templateUrl: './select-branch.component.html',
  styleUrls: ['./select-branch.component.scss']
})
export class SelectBranchComponent implements OnInit {
  lBranches: CodeTable[] = [];
  selectedBranch!: CodeTable;
  constructor(private router:Router) { }
  ngOnInit(): void {
    const userBranches = localStorage.getItem('userBranches');
    if (userBranches !== null) {
      this.lBranches = JSON.parse(userBranches);
    }
  }
  continue(){
    localStorage.setItem('selectedBranch',this.selectedBranch.iBranchId.toString());
    this.router.navigateByUrl('surveys');
  }
}
