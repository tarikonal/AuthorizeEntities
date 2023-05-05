import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Kod_BirlikTip } from './models/Kod_BirlikTip';
import { Kod_BirlikTipService } from './services/Kod_BirlikTip.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-kod_BirlikTip',
	templateUrl: './kod_BirlikTip.component.html',
	styleUrls: ['./kod_BirlikTip.component.scss']
})
export class Kod_BirlikTipComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id', 'update','delete'];

	kod_BirlikTipList:Kod_BirlikTip[];
	kod_BirlikTip:Kod_BirlikTip=new Kod_BirlikTip();

	kod_BirlikTipAddForm: FormGroup;


	kod_BirlikTipId:number;

	constructor(private kod_BirlikTipService:Kod_BirlikTipService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getKod_BirlikTipList();
    }

	ngOnInit() {

		this.createKod_BirlikTipAddForm();
	}


	getKod_BirlikTipList() {
		this.kod_BirlikTipService.getKod_BirlikTipList().subscribe(data => {
			this.kod_BirlikTipList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.kod_BirlikTipAddForm.valid) {
			this.kod_BirlikTip = Object.assign({}, this.kod_BirlikTipAddForm.value)

			if (this.kod_BirlikTip.id == 0)
				this.addKod_BirlikTip();
			else
				this.updateKod_BirlikTip();
		}

	}

	addKod_BirlikTip(){

		this.kod_BirlikTipService.addKod_BirlikTip(this.kod_BirlikTip).subscribe(data => {
			this.getKod_BirlikTipList();
			this.kod_BirlikTip = new Kod_BirlikTip();
			jQuery('#kod_birliktip').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.kod_BirlikTipAddForm);

		})

	}

	updateKod_BirlikTip(){

		this.kod_BirlikTipService.updateKod_BirlikTip(this.kod_BirlikTip).subscribe(data => {

			var index=this.kod_BirlikTipList.findIndex(x=>x.id==this.kod_BirlikTip.id);
			this.kod_BirlikTipList[index]=this.kod_BirlikTip;
			this.dataSource = new MatTableDataSource(this.kod_BirlikTipList);
            this.configDataTable();
			this.kod_BirlikTip = new Kod_BirlikTip();
			jQuery('#kod_birliktip').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.kod_BirlikTipAddForm);

		})

	}

	createKod_BirlikTipAddForm() {
		this.kod_BirlikTipAddForm = this.formBuilder.group({		
			id : [0]
		})
	}

	deleteKod_BirlikTip(kod_BirlikTipId:number){
		this.kod_BirlikTipService.deleteKod_BirlikTip(kod_BirlikTipId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.kod_BirlikTipList=this.kod_BirlikTipList.filter(x=> x.id!=kod_BirlikTipId);
			this.dataSource = new MatTableDataSource(this.kod_BirlikTipList);
			this.configDataTable();
		})
	}

	getKod_BirlikTipById(kod_BirlikTipId:number){
		this.clearFormGroup(this.kod_BirlikTipAddForm);
		this.kod_BirlikTipService.getKod_BirlikTipById(kod_BirlikTipId).subscribe(data=>{
			this.kod_BirlikTip=data;
			this.kod_BirlikTipAddForm.patchValue(data);
		})
	}


	clearFormGroup(group: FormGroup) {

		group.markAsUntouched();
		group.reset();

		Object.keys(group.controls).forEach(key => {
			group.get(key).setErrors(null);
			if (key == 'id')
				group.get(key).setValue(0);
		});
	}

	checkClaim(claim:string):boolean{
		return this.authService.claimGuard(claim)
	}

	configDataTable(): void {
		this.dataSource.paginator = this.paginator;
		this.dataSource.sort = this.sort;
	}

	applyFilter(event: Event) {
		const filterValue = (event.target as HTMLInputElement).value;
		this.dataSource.filter = filterValue.trim().toLowerCase();

		if (this.dataSource.paginator) {
			this.dataSource.paginator.firstPage();
		}
	}

  }
