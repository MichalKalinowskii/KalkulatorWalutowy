import { Component, EventEmitter, Output } from '@angular/core';
import { UserCredentials } from '../../models/userCredentials';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../services/userService';
import { HttpResponse, HttpStatusCode } from '@angular/common/http';

@Component({
  selector: 'app-user-auth',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './user-auth.component.html',
  styleUrls: ['./user-auth.component.css']
})
export class UserAuthComponent {
  @Output() public userLoggedInEvent = new EventEmitter<string>();
  @Output() public closeModalEvent = new EventEmitter<boolean>();

  loginCredentials: UserCredentials = { username: '', password: '' };
  registerCredentials: UserCredentials = { username: '', password: '' };
  showLoginModal: boolean = false;
  showRegisterModal: boolean = false;

  constructor(private userService: UserService) { }

  onLogin() {
    this.userService.login(this.loginCredentials).subscribe({
      next: data => {
        console.log('Login successful');
        this.userLoggedInEvent.emit(this.loginCredentials.username);
        this.closeModal();
      },
      error: err => {
        console.log('Login error:', err);
      },
    });    
  }

  onRegister() {
    this.userService.register(this.registerCredentials).subscribe({
      next: data => {
        console.log('Registration successful');
        this.userLoggedInEvent.emit(this.registerCredentials.username);
        this.closeModal();
      },
      error: err => {
        console.log('Registration error:', err);
      },
    });    
  }

  openModal(type: string) {
    if (type === 'login') {
      this.showLoginModal = true;
    } else {
      this.showRegisterModal = true;
    }
  }

  backToButtons() {
    this.showLoginModal = false;
    this.showRegisterModal = false;
  }

  closeModal() {
    this.closeModalEvent.emit(true);
  }
}