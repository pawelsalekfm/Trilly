import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ContactInfoCardsComponent } from './contact-info-cards.component';

describe('ContactInfoCardsComponent', () => {
  let component: ContactInfoCardsComponent;
  let fixture: ComponentFixture<ContactInfoCardsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ContactInfoCardsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ContactInfoCardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
