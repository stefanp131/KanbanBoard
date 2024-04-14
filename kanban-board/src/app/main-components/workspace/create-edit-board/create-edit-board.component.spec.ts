import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEditBoardComponent } from './create-edit-board.component';

describe('CreateEditBoardComponent', () => {
  let component: CreateEditBoardComponent;
  let fixture: ComponentFixture<CreateEditBoardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateEditBoardComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CreateEditBoardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
