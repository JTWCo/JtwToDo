import { Injectable } from '@angular/core';
import { IToDo } from './to-do';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';


@Injectable()
export class ToDoService {

  private _todoUrl = 'https://jtwtodoapi.azurewebsites.net/jtwtodoapi/ToDo';

  constructor(private _http: HttpClient) { }

  getToDos() : Observable<IToDo[]>{
    return this._http.get<IToDo[]>(this._todoUrl+'/GetAll/');
  }
}
