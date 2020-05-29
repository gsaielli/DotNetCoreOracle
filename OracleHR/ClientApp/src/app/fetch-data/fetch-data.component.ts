import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public forecasts: WeatherForecast[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}

interface WeatherForecast {
  Employee_id: number;
  First_name: string;
  Last_name: string;
  Email: string;
  Phone_number: string;
  Hire_date: Date;
  Job_id: string;
  Salary: number;
  Commission_pct: number;
  Manager_id: number;
  Department_id: number;
}
