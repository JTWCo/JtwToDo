import { Injectable } from '@angular/core';
import { IToDo } from './to-do';
import { Observable } from 'rxjs/Observable';
import {HttpClient} from "@angular/common/http";
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class ToDoService {

  //TODO:should not be hardcoded
  private _todoUrl = 'http://jtwtodoapi.azurewebsites.net/jtwtodoapi/ToDo'; 
  //private _todoUrl = 'http://localhost:57056/jtwtodoapi/ToDo';

  constructor(private http: HttpClient) { }

  getToDos() : Observable<IToDo[]>{
    return this.http.get<IToDo[]>(this._todoUrl + '/GetAll/');
  }

  updateToDo(todo: IToDo): Observable<IToDo> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });

    if (todo.id === 0) {
      return this.http.post<IToDo>(this._todoUrl + '/Post/', todo, { headers });
    } else {
      return this.http.put<IToDo>(this._todoUrl + '/Put/' + todo.id + '/', todo, { headers });
    }
  }
  deleteToDo(id: number): Observable<IToDo> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
    return this.http.delete<IToDo>(this._todoUrl + '/Delete/' + id + '/', { headers });
  }
}
