import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Sidebar } from 'primeng/sidebar';
import { UsersService } from 'src/app/services/users.service';
import { userModel } from 'src/models/users.model';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
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
    this.frmUsers = new FormGroup({
      userName: new FormControl(),
      code: new FormControl(),
    })
  }
  login() {
    this.srv.getUserById(this.frmUsers.controls['userName'].value, this.frmUsers.controls['code'].value).subscribe((res: userModel) => {
      this.user = res;
      if (this.user != null) {
        localStorage.setItem("jwt-token", this.user.token)
        this.router.navigateByUrl("surveys");
      }
      else
        alert("not permission")
      console.log(this.user);
    })



  }
}
