import { Component, Input, OnInit } from '@angular/core';
import {ToDoService } from '../to-do/to-do.service';
import { IToDo } from "../to-do/to-do";
import {SortPipe} from '../pipes/sort.pipe';

@Component({
  selector: 'app-to-do-list',
  templateUrl: './to-do-list.component.html',
  styleUrls: ['./to-do-list.component.scss'],
  providers:[ToDoService, SortPipe]
})
export class ToDoListComponent implements OnInit {
  showCompleted: boolean = false;
  completedButtonText: string = "Show Completed";
  errorMessage: string;
  nothingToShow: boolean = true;
  toDoToAdd: IToDo = <IToDo> {
    id: 0
  };
  addToDo: boolean = false;

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

  toggleAdd(): void {
    this.addToDo = !this.addToDo;
  }

  constructor(private _toDoService: ToDoService, private sortPipe: SortPipe) { }

  ngOnInit(): void {

    this.populateList();
  }

  private populateList(): void {
    this._toDoService.getToDos().subscribe(tds => {
      this.todos = tds;

      this.sortPipe.transform(this.todos, 'dueDate', 'end');

      for (let td of this.todos) {
        if (!td.completed) {
          this.nothingToShow = false;
        }
      }
    }, error => this.errorMessage = <any>error);
  }

  updateList(): void {
    this.populateList();
    this.toDoToAdd = <IToDo>{ id: 0 }
    this.addToDo = false;
  }
}
