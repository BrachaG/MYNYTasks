import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Route, Router } from '@angular/router';
import { LoginComponent } from '../pages/login/login.component';
import { userModel } from 'src/models/users.model';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  constructor(public router:Router){

  }
  @Output() showImageParent: EventEmitter<any> = new EventEmitter();
  @Input() showImage: any
  userName: string | null ='';
  userPermission:string | null='';
  setShowImage() {
    this.showImage = !this.showImage
    this.showImageParent.emit(this.showImage);
  }
  logOut() {
    localStorage.removeItem("jwt-token");
    this.router.navigate(["login"])
  }
  changeBranch(){
    this.router.navigate(["select-branch"])
  }
  ngOnInit() { 
    this.userName=localStorage.getItem('userName');
    this.userPermission=localStorage.getItem('userPermission');
 }
  
}
