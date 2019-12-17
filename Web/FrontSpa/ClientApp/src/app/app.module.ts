import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './routes/home/home.component';
import { HeaderTopComponent } from './widgets/header-top/header-top.component';
import { HeaderMainComponent } from './widgets/header-main/header-main.component';
import { MainNaveComponent } from './widgets/main-nave/main-nave.component';
import { BannerComponent } from './widgets/banner/banner.component';
import { CharacteristicsComponent } from './widgets/characteristics/characteristics.component';
import { DealsOfTheWeekComponent } from './widgets/deals-of-the-week/deals-of-the-week.component';
import { FeaturedComponent } from './widgets/featured/featured.component';
import { PopularCategoriesComponent } from './widgets/popular-categories/popular-categories.component';
import { Banner2Component } from './widgets/banner2/banner2.component';
import { NewArrivalsComponent } from './widgets/new-arrivals/new-arrivals.component';
import { BestsellersComponent } from './widgets/bestsellers/bestsellers.component';
import { AdvertsComponent } from './widgets/adverts/adverts.component';
import { BrandsComponent } from './widgets/brands/brands.component';
import { NewsletterComponent } from './widgets/newsletter/newsletter.component';
import { FooterComponent } from './widgets/footer/footer.component';
import { CopyrightComponent } from './widgets/copyright/copyright.component';
import { CategoriesComponent } from './widgets/categories/categories.component';
import { ContactComponent } from './routes/contact/contact.component';
import { ContactInfoCardsComponent } from './widgets/contact-info-cards/contact-info-cards.component';
import { ProductDetailsComponent } from './routes/product-details/product-details.component';
import { RecentlyViewedProductsComponent } from './widgets/recently-viewed-products/recently-viewed-products.component';
import { ProductsInCategoryComponent } from './routes/products-in-category/products-in-category.component';
import { CartComponent } from './routes/cart/cart.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderTopComponent,
    HeaderMainComponent,
    MainNaveComponent,
    BannerComponent,
    CharacteristicsComponent,
    DealsOfTheWeekComponent,
    FeaturedComponent,
    PopularCategoriesComponent,
    Banner2Component,
    NewArrivalsComponent,
    BestsellersComponent,
    AdvertsComponent,
    BrandsComponent,
    NewsletterComponent,
    FooterComponent,
    CopyrightComponent,
    CategoriesComponent,
    ContactComponent,
    ContactInfoCardsComponent,
    ProductDetailsComponent,
    RecentlyViewedProductsComponent,
    ProductsInCategoryComponent,
    CartComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
