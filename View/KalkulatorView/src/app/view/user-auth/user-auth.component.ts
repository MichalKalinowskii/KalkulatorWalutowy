import { Component } from '@angular/core';
import { UserCredentials } from '../../models/userCredentials';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-auth',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './user-auth.component.html',
  styleUrls: ['./user-auth.component.css']
})
export class UserAuthComponent {
  loginCredentials: UserCredentials = { username: '', password: '' };
  registerCredentials: UserCredentials = { username: '', password: '' };

  onLogin() {
    // Handle login logic here
    console.log('Login:', this.loginCredentials);
  }

  onRegister() {
    // Handle registration logic here
    console.log('Register:', this.registerCredentials);
  }
}