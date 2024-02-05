import { Component, OnInit } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {Router} from "@angular/router";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-studenti',
  templateUrl: './studenti.component.html',
  styleUrls: ['./studenti.component.css']
})
export class StudentiComponent implements OnInit {

  title:string = 'angularFIT2';
  ime_prezime:string = '';
  opstina: string = '';
  studentPodaci: any;
  filter_ime_prezime: boolean;
  filter_opstina: boolean;
  enableIme:boolean=true;
  enableOpstina:boolean=true;
  studentEditData:any;

  openEditModal:boolean=false;
  defaultOpstina:any;

  openAddModal:boolean=false;

  constructor(private httpKlijent: HttpClient, private router: Router) {
  }
  fenableIme(){
     this.enableIme=!this.enableIme;
  }
  fenableOpstina(){
    this.enableOpstina=!this.enableOpstina;
  }
  testirajWebApi(params?:HttpParams) :void
  {
    let headers=new HttpHeaders().set("autentifikacijaToken",MojConfig.http_opcije().headers["autentifikacija-token"])

    this.httpKlijent.get(MojConfig.adresa_servera+ "/Student/GetAll",{headers,params}).subscribe(x=>{
      this.studentPodaci = x ;
      console.log(x)
    });
  }
  filtrirajPoImenu(){
    let params=new HttpParams().set("ime_prezime",this.ime_prezime);
    console.log(params)
    this.testirajWebApi(params)
  }
  filtrirajPoOpstini(){
    let params=new HttpParams().set("opstina",this.opstina);
    console.log(params)
    this.testirajWebApi(params)
  }
 otvoriEditModal(student:any,opstina:any)
 {
   this.studentEditData=student;
   this.defaultOpstina=opstina;
   this.openEditModal=true;


 }
 closeEditModal(){
    this.openEditModal=false;
 }
 closeAddModal(){
    this.openAddModal=false;
 }
 handleReload(){
    this.testirajWebApi();

 }
  ngOnInit(): void {
    this.testirajWebApi();

  }

  otvoriAddModal() {
   this.openAddModal=!this.openAddModal;
  }

  sakriZapis(student:any) {
    student.hidden=true;

    let body={
        id:student.id,
        isDeleted:student.hidden
      }
      this.httpKlijent.delete(MojConfig.adresa_servera+"/Student/SoftDelete",{body}).subscribe(res=>{
        console.log(res);
      })
  }

  otvoriMaticnuKnjigu(s: any) {
    let id:number=s.id
    this.router.navigate(["/student-maticnaknjiga/"+id]);
  }
}
