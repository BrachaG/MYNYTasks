import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TargetButtonsComponent } from './target-buttons.component';

describe('TargetButtonsComponent', () => {
  let component: TargetButtonsComponent;
  let fixture: ComponentFixture<TargetButtonsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TargetButtonsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TargetButtonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
