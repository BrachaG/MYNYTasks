import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Route, Router } from '@angular/router';
import { LoginComponent } from '../pages/login/login.component';
import { CodeTable } from '../../models/CodeTable.model';
import { SettingsService } from '../services/settings.service';
import { UsersService } from '../services/users.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  constructor(public router: Router, public _settingsService: SettingsService, private _usersService: UsersService) { }

  @Output() showImageParent: EventEmitter<any> = new EventEmitter();
  @Input() showImage: any
  userName: string = '';
  userPermission: string | null = '';
  lBranches: CodeTable[] = [];
  selectedBranch!: CodeTable;
  selectedBranchName: string | undefined = "";
  selectedBranchNumber: number | null = 0;
  change: boolean = false;
  userEmail:string="";

  setShowImage() {
    this.showImage = !this.showImage
    this.showImageParent.emit(this.showImage);
  }

  logOut() {
    localStorage.clear();
    this.router.navigate(["login"])
  }
  // changeBranch(){
  //   this.change=true;
  // }
  isMenuOpen = false;

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }
  onBranchSelected() {
    localStorage.setItem('selectedBranch', this.selectedBranch.iBranchId.toString());
    this.change = false;
    window.location.reload()
  }

  ngOnInit() {
    this.userName = localStorage.getItem('userName') ?? "";
    this.userPermission = localStorage.getItem('userPermission');
    const userBranches = localStorage.getItem('userBranches');
    if (userBranches !== null) {
      this.lBranches = JSON.parse(userBranches);
    }
    this.selectedBranchNumber = Number(localStorage.getItem('selectedBranch'));
    this.selectedBranchName = this.lBranches.find((obj) => {
      return obj.iBranchId === this.selectedBranchNumber;
    })?.nvBranchName;
    //לא קיימת קריאה כזו בסרבר
    // this._usersService.getEmailById().subscribe({
    //   next: (emailId: any) => {
    //     this.userEmail=emailId.value
    //     console.log('Email ID:', emailId);
    //   },
    //   error: (error: any) => {
    //     console.error('Error:', error);
    //   },
    //   complete: () => {
    //     // Handle completion if needed
    //   },
    // });
    
  }
  changeBranch(b: CodeTable) {
    localStorage.setItem('selectedBranch', b.iBranchId.toString());
    this.selectedBranchName = b.nvBranchName;
    this.router.navigateByUrl('surveys');

  }
}
