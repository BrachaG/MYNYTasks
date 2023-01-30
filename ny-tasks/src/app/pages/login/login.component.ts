import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { UsersService } from 'src/app/services/users.service';
import { userModel } from 'src/models/users.model';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user:any
  constructor( private srv :UsersService) { 
  }

  frmUsers: FormGroup = new FormGroup({
    userName: new FormControl(''),
    code: new FormControl(''),
   } )
  ngOnInit(): void {
    this.frmUsers=new FormGroup({
      userName:new FormControl(),
      code: new FormControl(),
     })
  }
   login(){ 
    this.srv.getUserById(this.frmUsers.controls['userName'].value,this.frmUsers.controls['code'].value).subscribe((res: userModel) => {
      this.user = res;
      if(this.user.iUserId>=0)
      alert("welcome")
      else
      alert("not permission")
     console.log(this.user);
    })
    
   
   
  }
}
