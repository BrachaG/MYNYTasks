import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BarButtonsComponent } from './bar-buttons.component';

describe('BarButtonsComponent', () => {
  let component: BarButtonsComponent;
  let fixture: ComponentFixture<BarButtonsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BarButtonsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BarButtonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
