import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Route, Router } from '@angular/router';
import { LoginComponent } from '../pages/login/login.component';
import { CodeTable } from '../../models/CodeTable.model';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  constructor(public router:Router) {}
 
  @Output() showImageParent: EventEmitter<any> = new EventEmitter();
  @Input() showImage: any
  userName: string | null ='';
  userPermission:string | null='';
  lBranches: CodeTable[] = [];
  selectedBranch!: CodeTable;
  change:boolean=false;
  setShowImage() {
    this.showImage = !this.showImage
    this.showImageParent.emit(this.showImage);
  }
  logOut() {
    localStorage.clear();
    this.router.navigate(["login"])
  }
  changeBranch(){
    this.change=true;
  }
  onBranchSelected(){
    localStorage.setItem('selectedBranch',this.selectedBranch.iBranchId.toString());
    this.change=false;
    window.location.reload()  
  }
  ngOnInit() { 
    this.userName=localStorage.getItem('userName');
    this.userPermission=localStorage.getItem('userPermission');
    const userBranches = localStorage.getItem('userBranches');
    if (userBranches !== null) {
      this.lBranches = JSON.parse(userBranches);
    }
 }
  
}
