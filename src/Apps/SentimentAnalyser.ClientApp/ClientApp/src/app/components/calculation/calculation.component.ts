import { Component, OnInit } from '@angular/core';
import { CalculationService } from 'src/app/services/calculation.service';

@Component({
  selector: 'calculation',
  templateUrl: './calculation.component.html',
  styleUrls: ['./calculation.component.scss']
})
export class CalculationComponent {
  public fileSelected: boolean;
  public textConnotation: string;
  public textConnotationCssClass: string;
  public isSentimentCalculated: boolean;
  public text: string;
  public fileContent: string;
  public fileName: string;

  constructor(private calculationService: CalculationService) {
    this.initializeTextScreen();
  }

  initializeTextScreen() {
    this.text = '';
    this.isSentimentCalculated = false;
  }

  initializeFileScreen() {
    this.fileName = '';
    this.fileContent = '';
    this.isSentimentCalculated = false;
  }

  toggleChangeInputType(event: any) {
    if (this.isSentimentCalculated) {
      this.isSentimentCalculated = false;
    }

    if (event.value === "file") {
      this.fileSelected = true;
      this.initializeFileScreen();
    } else {
      this.fileSelected = false;
      this.initializeTextScreen();
    }
  }

  openFile(){
    document.querySelector('input').click()
  }

  handle(event){
    let file = event.target.files[0];
    this.fileName = file.name;
    this.fileContent = '';
    let fileReader: FileReader = new FileReader();
    let self = this;
    fileReader.onloadend = function(x) {
      var buffer = fileReader.result;
      self.fileContent += buffer.toString();
    }
    fileReader.readAsText(file);
  }

  getTextConnotation() {
    if (this.fileSelected) {
      this.text = this.fileContent;
    } 

    this.calculationService.calculateTextConnotation({ text: this.text })
        .subscribe((value) => {
          this.extractConnotationInfo(value.data);
          
          this.isSentimentCalculated = true; 
        });
  }

  extractConnotationInfo(data: number) {
    switch(data) {
      case 1:
        this.textConnotation = 'positive';
        this.textConnotationCssClass = 'green';
        break;
      case -1:
        this.textConnotation = 'negative';
        this.textConnotationCssClass = 'red';
        break;
      default:
        this.textConnotation = 'neutral';
        this.textConnotationCssClass = 'black';
    }
  }
}
