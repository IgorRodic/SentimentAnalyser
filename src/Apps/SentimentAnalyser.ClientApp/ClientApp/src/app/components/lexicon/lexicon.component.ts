import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog'
import { SentimentDto } from 'src/app/api/lexicon.api';
import { AddSentimentDialog } from '../add-sentiment-dialog/add-sentiment.dialog';
import { DeleteDialog } from '../delete-dialog/delete.dialog';
import { LexiconState } from 'src/app/store/lexicon.state';
import { select, Store } from '@ngrx/store';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { getAllSentiments } from 'src/app/store/lexicon.actions';
import { allSentiments, getError } from 'src/app/store/lexicon.selectors';
import { EditSentimentDialog } from '../edit-sentiment-dialog/edit-sentiment.dialog';

@Component({
  selector: 'lexicon',
  templateUrl: './lexicon.component.html',
  styleUrls: ['./lexicon.component.scss']
})
export class LexiconComponent implements OnInit, AfterViewInit, OnDestroy {
  displayedColumns: string[] = ['id', 'word', 'score', 'actions'];
  dataSource = new MatTableDataSource<SentimentDto>();
  allSentiments: SentimentDto[];
  error: string = '';
  showError: boolean = false;
  private unsubscribe$ = new Subject();


  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private dialog: MatDialog, 
    private store: Store<LexiconState>) { 
      this.store.pipe(
        select(allSentiments),
        takeUntil(this.unsubscribe$)
      )
      .subscribe((value) => {
        this.dataSource.data = value;
        this.allSentiments = value;
      });

      this.store.pipe(
        select(getError),
      )
      .subscribe((value) => {
        if (!value) {
          return;
        }

        this.error = value;
        this.showError = true;
      });
    }

  ngOnInit() {
    this.store.dispatch(getAllSentiments());
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  filterAll() {
    this.dataSource.data = this.allSentiments;
    this.hideError();
  }

  filterPositive() {
    this.dataSource.data = this.allSentiments.filter(s => s.sentimentScore > 0);
    this.hideError();
  }

  filterNegative() {
    this.dataSource.data = this.allSentiments.filter(s => s.sentimentScore < 0);
    this.hideError();
  }

  hideError() {
    this.showError = false;
  }

  getWordConnotation(sentiment: SentimentDto) {
    if (sentiment.sentimentScore > 0) {
      return "green"
    } else if (sentiment.sentimentScore < 0) {
      return "red";
    }
      
    return "";
  }

  addSentiment(): void {
    const dialogRef = this.dialog.open(AddSentimentDialog, {
      width: '250px'
    });

    this.hideError();
  }

  editSentiment(sentiment: SentimentDto): void {
    const dialogRef = this.dialog.open(EditSentimentDialog, {
      width: '250px',
      data: {
        id: sentiment.id,
        word: sentiment.word,
        sentimentScore: sentiment.sentimentScore
      }
    });

    this.hideError();
  }

  deleteSentiment(sentimentId: string): void {
    const dialogRef = this.dialog.open(DeleteDialog, {
      width: '250px',
      data: {
        sentimentId: sentimentId
      }
    });

    this.hideError();
  }

  getPageSizeOptions() {
    const dataSize = this.dataSource.data.length;
    if (dataSize <= 10) {
      return [5, 10];
    } else if (dataSize > 10 && dataSize <= 20) {
      return [5, 10, 20];
    } else if (dataSize > 20 && dataSize <= 50) {
      return [5, 10, 20, 50];
    } else if (dataSize > 50 && dataSize <= 100) {
      return [5, 10, 20, 50, 100];
    } else if (dataSize > 100 && dataSize <= 200) {
      return [5, 10, 20, 50, 100, 200];
    } else if (dataSize > 200 && dataSize <= 500) {
      return [5, 10, 20, 50, 100, 200, 500];
    } else if (dataSize > 500 && dataSize < 1000) {
      return [5, 10, 20, 50, 100, 200, 500, 1000];
    } else {
      return [5, 10, 20, 50, 100, 200, 500, 1000, dataSize];
    }
  }
}
