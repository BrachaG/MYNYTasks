import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Sidebar } from 'primeng/sidebar';
import { UsersService } from 'src/app/services/users.service';
import { userModel } from 'src/models/users.model';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  user: any
  constructor(private srv: UsersService, private router: Router) {
  }

  frmUsers: FormGroup = new FormGroup({
    userName: new FormControl(''),
    code: new FormControl(''),
  })
  ngOnInit(): void {
    localStorage.clear()
    this.frmUsers = new FormGroup({
      userName: new FormControl(),
      code: new FormControl(),
    })
  }
  login() {
    this.srv.getUserById(this.frmUsers.controls['userName'].value, this.frmUsers.controls['code'].value).subscribe((res: userModel) => {
      this.user = res; 
      if (this.user != null) {
        localStorage.setItem('userName',this.user.nvUserName);
        localStorage.setItem('userPermission',this.user.iPermissionLevelId);
        localStorage.setItem("jwt-token", this.user.token);
        if (this.user.iPermissionLevelId==2 && this.user.lBranches.length>1) {
          localStorage.setItem('userBranches',JSON.stringify(this.user.lBranches))
          this.router.navigateByUrl('select-branch');
        }
        else
          this.router.navigateByUrl('surveys');
      }
      else
        alert("not permission")
      console.log(this.user);
    })
  }
}
