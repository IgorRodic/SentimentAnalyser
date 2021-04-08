import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Store } from "@ngrx/store";
import { deleteSentiment } from "src/app/store/lexicon.actions";
import { LexiconState } from "src/app/store/lexicon.state";

@Component({
    selector: 'delete-dialog',
    templateUrl: 'delete.dialog.html',
    styleUrls: ['./delete.dialog.scss']
})
export class DeleteDialog {

    public sentimentId: string;

    constructor(
        public dialogRef: MatDialogRef<DeleteDialog>,
        @Inject(MAT_DIALOG_DATA) public data: any,
        private store: Store<LexiconState>
    ) {
        this.sentimentId = data.sentimentId;
    }

    deleteSentiment() {
        this.store.dispatch(deleteSentiment({ sentimentId: this.sentimentId }));
        this.closeDialog();
    }

    closeDialog(): void {
        this.dialogRef.close();
    }

}