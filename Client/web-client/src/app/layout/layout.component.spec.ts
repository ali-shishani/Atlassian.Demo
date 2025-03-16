import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { LayoutComponent } from './layout.component';

import { LayoutModule } from './layout.module';
import { AppModule } from "../app.module";

describe('LayoutComponent', () => {
  
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule, 
        AppModule,
        LayoutModule,
      ],
      declarations: [
        LayoutComponent
      ],
    })
    .compileComponents();
    
  });

  it('should create', () => {
    const fixture = TestBed.createComponent(LayoutComponent);
    const component = fixture.componentInstance;
    fixture.detectChanges();
    expect(component).toBeTruthy();
  });

  it('should render title', () => {
      const fixture = TestBed.createComponent(LayoutComponent);
      const component = fixture.componentInstance;
      fixture.detectChanges();
      const compiled = fixture.nativeElement as HTMLElement;
      expect(compiled.querySelector('.layout-header-title')?.textContent).toContain('The mood rating app');
    });
});
