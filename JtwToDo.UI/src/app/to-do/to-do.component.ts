import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-to-do',
  templateUrl: './to-do.component.html',
  styleUrls: ['./to-do.component.scss']
})

export class ToDoComponent implements OnInit {
  @Input() showIfCompleted: boolean;
  showToDo: boolean = true;
  editToDo: boolean = false;
  @Input() selectToDo: boolean = false;
  showEditOptions : boolean = false;

  @Input() todo: any;

  constructor() { }

  ngOnInit() {
  }

  toggleSelected(): void {
    this.selectToDo = !this.selectToDo;
  }

  toggleEdited(): void {
    this.editToDo = !this.editToDo;
  }

  saveChanges(): void {
    this.toggleEdited();
  }
  ngOnChanges(): void {
    this.showToDo = (this.todo.Completed && this.showIfCompleted) || !this.todo.Completed;
  }

}
