import { Component, Input, OnInit } from '@angular/core';
import Todoservice = require("../to-do/to-do.service");
import ToDoService = Todoservice.ToDoService;
import Todo = require("../to-do/to-do");
import IToDo = Todo.IToDo;


@Component({
  selector: 'app-to-do-list',
  templateUrl: './to-do-list.component.html',
  styleUrls: ['./to-do-list.component.scss'],
  providers:[ToDoService]
})
export class ToDoListComponent implements OnInit {
  showCompleted: boolean = false;
  completedButtonText: string = "Show Completed";
  errorMessage: string;

  @Input() todos: IToDo[] = [];

  toggleCompleted(): void {
    this.showCompleted = !this.showCompleted;
  }

  constructor(private _toDoService: ToDoService) { }

  ngOnInit(): void {

    this._toDoService.getToDos().subscribe(tds=> this.todos = tds, error => this.errorMessage = <any>error);
  }

}
