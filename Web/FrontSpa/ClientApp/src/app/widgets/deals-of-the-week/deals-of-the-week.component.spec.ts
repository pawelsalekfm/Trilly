import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DealsOfTheWeekComponent } from './deals-of-the-week.component';

describe('DealsOfTheWeekComponent', () => {
  let component: DealsOfTheWeekComponent;
  let fixture: ComponentFixture<DealsOfTheWeekComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DealsOfTheWeekComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DealsOfTheWeekComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
