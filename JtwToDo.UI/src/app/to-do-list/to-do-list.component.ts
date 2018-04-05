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
  nothingToShow: boolean = true;

  @Input() todos: IToDo[] = [];

  @Input() listUpdated = false;

  toggleCompleted(): void {
    this.showCompleted = !this.showCompleted;
    this.nothingToShow = !this.showCompleted;
  }

  constructor(private _toDoService: ToDoService) { }

  ngOnInit(): void {

    this._toDoService.getToDos().subscribe(tds => {
      this.todos = tds;

      
      for (let td of this.todos) {
        if (!td.completed) {
          this.nothingToShow = false;
        }
      }
    }, error => this.errorMessage = <any>error);
  }

}
