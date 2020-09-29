import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Recipe } from 'schema-dts';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  constructor(
    private readonly httpClient: HttpClient
  ) { }

  list(): Observable<Recipe> {
    return this.httpClient.post<Recipe>(
      environment.recipeApiUrl + '/api/recipe/list',
      {
        test: 123
      }
    );
  }

  get(ids: string[]): Observable<Recipe> {
    return this.httpClient.post<Recipe>(
      environment.recipeApiUrl + '/api/recipe/get',
      {
        ids
      }
    )
  }
}
