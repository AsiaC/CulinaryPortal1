import { Directive, Input, OnInit, TemplateRef, ViewContainerRef  } from '@angular/core';
import { take } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';

@Directive({
  selector: '[hasRole]' // hasRole='["Admin"]'
})
export class HasRoleDirective implements OnInit{
  @Input() hasRole: string[];
  user: User;
  constructor(private viewContainerRef: ViewContainerRef, private templateRef: TemplateRef<any>, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
    })
   }

   ngOnInit(): void { 
    if (!this.user?.roles || this.user === null) {
      // Clear the view if no roles
      this.viewContainerRef.clear(); 
      return;
    }
    if (this.user?.roles.some(role => this.hasRole.includes(role))) {
      this.viewContainerRef.createEmbeddedView(this.templateRef);
    } else {
      this.viewContainerRef.clear();
    }
  }
}
