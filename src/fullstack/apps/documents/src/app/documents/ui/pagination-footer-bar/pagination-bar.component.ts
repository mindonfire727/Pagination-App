import { Component, EventEmitter, Input, Output, OnInit, OnDestroy, ChangeDetectionStrategy, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule  } from '@angular/common';
import { DestroyableDirective } from '../destroyable.directive';
import { NavigationButton } from '../../models/document-models';


@Component({
  selector: 'fullstack-pagination-bar',
  template: `<button *ngFor="let button of buttons; let i = index;"
  [ngClass]="{'clicked': button.clicked}"
  (click)="onPageClick(i)">
{{ button.value }}
</button>`,
  styleUrls: ['./pagination-bar.component.scss'],
  standalone: true,
  imports: [CommonModule],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PaginationBarComponent extends DestroyableDirective implements OnDestroy, OnChanges {
  @Output() selectedPage = new EventEmitter();
  @Input() numberOfPages = 1;
  public buttons: NavigationButton[] = []

  onPageClick(page:number){
    for (let i = 0; i < this.buttons.length; i++) {
      if (i === page) {
        this.buttons[i].clicked = true;
      } else {
        this.buttons[i].clicked = false;
      }
    }
    this.selectedPage.emit(page + 1);
  }

  ngOnChanges(changes: SimpleChanges) {
    if ('numberOfPages' in changes && !changes['numberOfPages'].firstChange){
      this.createButtons(changes['numberOfPages'].currentValue)
    }
  }

  private createButtons(amount: number): void {
    this.buttons = [];
    for (let i = 1; i <= amount; i++) {
      this.buttons.push({ value: i, clicked: i === 1 });
    }
  }

  override ngOnDestroy() {
    super.ngOnDestroy();
  }

}
