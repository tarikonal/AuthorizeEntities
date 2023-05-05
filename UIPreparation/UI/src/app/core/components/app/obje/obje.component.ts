import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Obje } from './models/Obje';
import { ObjeService } from './services/Obje.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-obje',
	templateUrl: './obje.component.html',
	styleUrls: ['./obje.component.scss']
})
export class ObjeComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','objeAdi','aciklama','durum','projeId', 'update','delete'];

	objeList:Obje[];
	obje:Obje=new Obje();

	objeAddForm: FormGroup;


	objeId:number;

	constructor(private objeService:ObjeService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getObjeList();
    }

	ngOnInit() {

		this.createObjeAddForm();
	}


	getObjeList() {
		this.objeService.getObjeList().subscribe(data => {
			this.objeList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.objeAddForm.valid) {
			this.obje = Object.assign({}, this.objeAddForm.value)

			if (this.obje.id == 0)
				this.addObje();
			else
				this.updateObje();
		}

	}

	addObje(){

		this.objeService.addObje(this.obje).subscribe(data => {
			this.getObjeList();
			this.obje = new Obje();
			jQuery('#obje').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.objeAddForm);

		})

	}

	updateObje(){

		this.objeService.updateObje(this.obje).subscribe(data => {

			var index=this.objeList.findIndex(x=>x.id==this.obje.id);
			this.objeList[index]=this.obje;
			this.dataSource = new MatTableDataSource(this.objeList);
            this.configDataTable();
			this.obje = new Obje();
			jQuery('#obje').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.objeAddForm);

		})

	}

	createObjeAddForm() {
		this.objeAddForm = this.formBuilder.group({		
			id : [0],
objeAdi : ["", Validators.required],
aciklama : ["", Validators.required],
durum : [false, Validators.required],
projeId : [0, Validators.required]
		})
	}

	deleteObje(objeId:number){
		this.objeService.deleteObje(objeId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.objeList=this.objeList.filter(x=> x.id!=objeId);
			this.dataSource = new MatTableDataSource(this.objeList);
			this.configDataTable();
		})
	}

	getObjeById(objeId:number){
		this.clearFormGroup(this.objeAddForm);
		this.objeService.getObjeById(objeId).subscribe(data=>{
			this.obje=data;
			this.objeAddForm.patchValue(data);
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
