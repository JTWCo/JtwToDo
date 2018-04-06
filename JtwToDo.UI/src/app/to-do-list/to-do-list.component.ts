import { Component, Input, OnInit } from '@angular/core';
import {ToDoService } from '../to-do/to-do.service';
import { IToDo } from "../to-do/to-do";


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

  private isNotCompleted(todo: IToDo) {
    return !todo.completed;
  }

  toggleCompleted(): void {
    this.showCompleted = !this.showCompleted;

    var notCompletedToDosCount = this.todos.find(this.isNotCompleted);
    var allToDosCount = this.todos.length;

    if (this.todos.length == 0) {
      this.nothingToShow = true;
    } else {
      if (!this.showCompleted) {
        if (notCompletedToDosCount == undefined) {
          this.nothingToShow = true;
        }
      }
    }
  }

  constructor(private _toDoService: ToDoService) { }

  ngOnInit(): void {

    this.populateList();
  }

  private populateList(): void {
    this._toDoService.getToDos().subscribe(tds => {
      this.todos = tds;


      for (let td of this.todos) {
        if (!td.completed) {
          this.nothingToShow = false;
        }
      }
    }, error => this.errorMessage = <any>error);
  }

  updateList(): void {
    this.populateList();
  }
}
