import { Injectable } from '@angular/core';
import { IToDo } from './to-do';
import { Observable } from 'rxjs/Observable';
import {HttpClient} from "@angular/common/http";
import { HttpHeaders } from '@angular/common/http';

//import 'rxjs/add/map';
//import 'rxjs/add/operator/share';


@Injectable()
export class ToDoService {

  private _todoUrl = 'https://jtwtodoapi.azurewebsites.net/jtwtodoapi/ToDo';

  constructor(private _http: HttpClient) { }

  getToDos() : Observable<IToDo[]>{
    return this._http.get<IToDo[]>(this._todoUrl + '/GetAll/');
  }

  updateToDo(todo: IToDo): void { 
    const headers = new HttpHeaders()
      .set("Content-Type", "application/json");
    this._http.put<IToDo>(this._todoUrl + '/Put/' +todo.id +'/', todo, { headers });
  }
}
