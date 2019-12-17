import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductsInCategoryComponent } from './products-in-category.component';

describe('ProductsInCategoryComponent', () => {
  let component: ProductsInCategoryComponent;
  let fixture: ComponentFixture<ProductsInCategoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProductsInCategoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductsInCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
