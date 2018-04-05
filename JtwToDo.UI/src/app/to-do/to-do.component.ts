import { Component, Input, OnInit } from '@angular/core';
import Todo = require("../to-do/to-do");
import IToDo = Todo.IToDo;
import Todoservice = require("../to-do/to-do.service");
import ToDoService = Todoservice.ToDoService;

@Component({
  selector: 'app-to-do',
  templateUrl: './to-do.component.html',
  styleUrls: ['./to-do.component.scss'],
  providers: [ToDoService]
})

export class ToDoComponent implements OnInit {
  @Input() showIfCompleted: boolean;
  showToDo: boolean = true;
  editToDo: boolean = false;
  @Input() selectToDo: boolean = false;
  errorMessage: string;
  
  @Input() todo: IToDo;

  constructor(private _toDoService: ToDoService) { }

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
    this._toDoService.updateToDo(this.todo);
  }

  saveChanges(): void {
    this._toDoService.updateToDo(this.todo);
    this.toggleEdited();
  }
  ngOnChanges(): void {
    this.showToDo = (this.todo.completed && this.showIfCompleted) || !this.todo.completed;
  }

}
