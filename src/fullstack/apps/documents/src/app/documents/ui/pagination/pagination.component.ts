import { ChangeDetectionStrategy, Component, EventEmitter, Output } from '@angular/core';
import { CommonModule  } from '@angular/common';


@Component({
  selector: 'fullstack-pagination',
  template: `
  <select (change)="onChange($event)">
  <option *ngFor="let value of values" [value]="value">{{ value }}</option>
</select>`,
  styleUrls: ['./pagination.component.scss'],
  standalone: true,
  imports: [CommonModule],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PaginationComponent {
  values = [10, 50, 100];
  @Output() selected = new EventEmitter();

  onChange(event: Event){
      this.selected.emit((event.target as HTMLSelectElement).value);
  }

}
