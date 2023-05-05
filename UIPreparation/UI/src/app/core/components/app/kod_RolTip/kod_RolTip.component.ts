import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Kod_RolTip } from './models/Kod_RolTip';
import { Kod_RolTipService } from './services/Kod_RolTip.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-kod_RolTip',
	templateUrl: './kod_RolTip.component.html',
	styleUrls: ['./kod_RolTip.component.scss']
})
export class Kod_RolTipComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id', 'update','delete'];

	kod_RolTipList:Kod_RolTip[];
	kod_RolTip:Kod_RolTip=new Kod_RolTip();

	kod_RolTipAddForm: FormGroup;


	kod_RolTipId:number;

	constructor(private kod_RolTipService:Kod_RolTipService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getKod_RolTipList();
    }

	ngOnInit() {

		this.createKod_RolTipAddForm();
	}


	getKod_RolTipList() {
		this.kod_RolTipService.getKod_RolTipList().subscribe(data => {
			this.kod_RolTipList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.kod_RolTipAddForm.valid) {
			this.kod_RolTip = Object.assign({}, this.kod_RolTipAddForm.value)

			if (this.kod_RolTip.id == 0)
				this.addKod_RolTip();
			else
				this.updateKod_RolTip();
		}

	}

	addKod_RolTip(){

		this.kod_RolTipService.addKod_RolTip(this.kod_RolTip).subscribe(data => {
			this.getKod_RolTipList();
			this.kod_RolTip = new Kod_RolTip();
			jQuery('#kod_roltip').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.kod_RolTipAddForm);

		})

	}

	updateKod_RolTip(){

		this.kod_RolTipService.updateKod_RolTip(this.kod_RolTip).subscribe(data => {

			var index=this.kod_RolTipList.findIndex(x=>x.id==this.kod_RolTip.id);
			this.kod_RolTipList[index]=this.kod_RolTip;
			this.dataSource = new MatTableDataSource(this.kod_RolTipList);
            this.configDataTable();
			this.kod_RolTip = new Kod_RolTip();
			jQuery('#kod_roltip').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.kod_RolTipAddForm);

		})

	}

	createKod_RolTipAddForm() {
		this.kod_RolTipAddForm = this.formBuilder.group({		
			id : [0]
		})
	}

	deleteKod_RolTip(kod_RolTipId:number){
		this.kod_RolTipService.deleteKod_RolTip(kod_RolTipId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.kod_RolTipList=this.kod_RolTipList.filter(x=> x.id!=kod_RolTipId);
			this.dataSource = new MatTableDataSource(this.kod_RolTipList);
			this.configDataTable();
		})
	}

	getKod_RolTipById(kod_RolTipId:number){
		this.clearFormGroup(this.kod_RolTipAddForm);
		this.kod_RolTipService.getKod_RolTipById(kod_RolTipId).subscribe(data=>{
			this.kod_RolTip=data;
			this.kod_RolTipAddForm.patchValue(data);
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
