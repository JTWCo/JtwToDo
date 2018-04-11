import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: "sort" })

//TODO:  this should also handle descending order sorting
export class SortPipe implements PipeTransform {
  transform(array: any[], field: string, nulls: string): any[] {
    array.sort((a: any, b: any) => {

      if (nulls === 'end') {
        if (a[field] < b[field] || b[field] == null) {
          return -1;
        } else if (a[field] > b[field] || a[field] == null) {
          return 1;
        } else {
          return 0;
        }
      } else {
        if (a[field] < b[field] || a[field] == null) {
          return -1;
        } else if (a[field] > b[field] || b[field] == null) {
          return 1;
        } else {
          return 0;
        }
      }
    });
    return array;
  }
}
