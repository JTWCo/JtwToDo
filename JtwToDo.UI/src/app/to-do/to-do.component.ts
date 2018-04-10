import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { IToDo } from '../to-do/to-do';
import {ToDoService } from '../to-do/to-do.service';

@Component({
  selector: 'app-to-do',
  templateUrl: './to-do.component.html',
  styleUrls: ['./to-do.component.scss'],
  providers: [ToDoService]
})

export class ToDoComponent implements OnInit {
  @Input() showIfCompleted: boolean;
  showToDo: boolean = true;
  @Input() editToDo: boolean = false;
  @Input() selectToDo: boolean = false;
  errorMessage: string;
  @Output() onTodoListUpdated: EventEmitter<any>;
  
  @Input() todo: IToDo;

  constructor(private _toDoService: ToDoService) {
    this.onTodoListUpdated = new EventEmitter();
  }

  ngOnInit() {
  }

  toggleSelected(): void {
    this.selectToDo = !this.selectToDo;
  }

  toggleEdited(): void {
    this.editToDo = !this.editToDo;
  }

  markComplete(): void {
    this.todo.completed = !this.todo.completed;
    this._toDoService.updateToDo(this.todo).subscribe(x => { this.errorMessage = "success" }, error => this.errorMessage = <any>error.errorMessage);
  }

  saveChanges(): void {
    this._toDoService.updateToDo(this.todo).subscribe(x => { this.errorMessage = "success" }, error => this.errorMessage = <any>error.errorMessage);
    this.toggleEdited();
  }

  deleteItem(): void {
    //this should really confirm that the user wants to delete before deleting
    this._toDoService.deleteToDo(this.todo.id).subscribe(x => {
      this.errorMessage = "success";
      this.onTodoListUpdated.emit();
    }, error => this.errorMessage = <any>error.errorMessage);
  }
  ngOnChanges(): void {
    this.showToDo = (this.todo.completed && this.showIfCompleted) || !this.todo.completed;
  }

}
