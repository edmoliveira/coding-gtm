import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GridProductComponent } from './grid-product/grid-product.component';
import { AuthGuard } from './helpers/auth-guard';
import { HomeComponent } from './home/home.component';
import { NotAuthorizedComponent } from './not-authorized/not-authorized.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ProductComponent } from './product/product.component';
import { SignInComponent } from './sign-in/sign-in.component';

const routes: Routes = [
  { 
    path: '', 
    component: HomeComponent,
    children: [
      { path: '', redirectTo: 'products', pathMatch: 'full' },
      { path: 'products', component: GridProductComponent },
      { path: 'product-detail/:id', component: ProductComponent }
    ],
    canActivate: [AuthGuard], canActivateChild: [AuthGuard]
  }
  , { path: 'login', component: SignInComponent,  }
  , { path: 'not-authorized', component: NotAuthorizedComponent,  }
  , { path: 'not-found', component: NotFoundComponent }
  , { path: '**', redirectTo: 'not-found' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
