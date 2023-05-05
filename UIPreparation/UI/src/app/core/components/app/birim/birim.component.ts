import { Component, AfterViewInit, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AlertifyService } from 'app/core/services/alertify.service';
import { LookUpService } from 'app/core/services/lookUp.service';
import { AuthService } from 'app/core/components/admin/login/services/auth.service';
import { Birim } from './models/birim';
import { BirimService } from './services/birim.service';
import { environment } from 'environments/environment';

declare var jQuery: any;

@Component({
	selector: 'app-birim',
	templateUrl: './birim.component.html',
	styleUrls: ['./birim.component.scss']
})
export class BirimComponent implements AfterViewInit, OnInit {
	
	dataSource: MatTableDataSource<any>;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	displayedColumns: string[] = ['id','keyValue','birimAdi','projeId','durum', 'update','delete'];

	birimList:Birim[];
	birim:Birim=new Birim();

	birimAddForm: FormGroup;


	birimId:number;

	constructor(private birimService:BirimService, private lookupService:LookUpService,private alertifyService:AlertifyService,private formBuilder: FormBuilder, private authService:AuthService) { }

    ngAfterViewInit(): void {
        this.getBirimList();
    }

	ngOnInit() {

		this.createBirimAddForm();
	}


	getBirimList() {
		this.birimService.getBirimList().subscribe(data => {
			this.birimList = data;
			this.dataSource = new MatTableDataSource(data);
            this.configDataTable();
		});
	}

	save(){

		if (this.birimAddForm.valid) {
			this.birim = Object.assign({}, this.birimAddForm.value)

			if (this.birim.id == 0)
				this.addBirim();
			else
				this.updateBirim();
		}

	}

	addBirim(){

		this.birimService.addBirim(this.birim).subscribe(data => {
			this.getBirimList();
			this.birim = new Birim();
			jQuery('#birim').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.birimAddForm);

		})

	}

	updateBirim(){

		this.birimService.updateBirim(this.birim).subscribe(data => {

			var index=this.birimList.findIndex(x=>x.id==this.birim.id);
			this.birimList[index]=this.birim;
			this.dataSource = new MatTableDataSource(this.birimList);
            this.configDataTable();
			this.birim = new Birim();
			jQuery('#birim').modal('hide');
			this.alertifyService.success(data);
			this.clearFormGroup(this.birimAddForm);

		})

	}

	createBirimAddForm() {
		this.birimAddForm = this.formBuilder.group({		
			id : [0],
keyValue : ["", Validators.required],
birimAdi : ["", Validators.required],
projeId : [0, Validators.required],
durum : [false, Validators.required]
		})
	}

	deleteBirim(birimId:number){
		this.birimService.deleteBirim(birimId).subscribe(data=>{
			this.alertifyService.success(data.toString());
			this.birimList=this.birimList.filter(x=> x.id!=birimId);
			this.dataSource = new MatTableDataSource(this.birimList);
			this.configDataTable();
		})
	}

	getBirimById(birimId:number){
		this.clearFormGroup(this.birimAddForm);
		this.birimService.getBirimById(birimId).subscribe(data=>{
			this.birim=data;
			this.birimAddForm.patchValue(data);
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
