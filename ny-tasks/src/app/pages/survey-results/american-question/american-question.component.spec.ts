import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AmericanQuestionComponent } from './american-question.component';

describe('AmericanQuestionComponent', () => {
  let component: AmericanQuestionComponent;
  let fixture: ComponentFixture<AmericanQuestionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AmericanQuestionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AmericanQuestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
