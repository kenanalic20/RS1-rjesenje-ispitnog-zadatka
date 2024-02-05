import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {MojConfig} from "../moj-config";
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {identity} from "rxjs";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-student-maticnaknjiga',
  templateUrl: './student-maticnaknjiga.component.html',
  styleUrls: ['./student-maticnaknjiga.component.css']
})
export class StudentMaticnaknjigaComponent implements OnInit {

  studentId:any;
  studentPodatci:any=[];
  hideNoviSemestar:boolean=false;
  akademskaGodina:any=[];
  selectedAkademskaGodina:any=1;
  datumUpisa: any=new Date().toISOString().split('T')[0];
  godinaStudija: any;
  cijenaSkolarine: any;
  obnova: any=false;
  maticnaKnjigaPodatci:any=[];


  hideOvjeraSemestar: boolean=false;
  datumOvjere: any=new Date().toISOString().split('T')[0];
  napomena: any;

  selectedMaticna:any;
  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) {}
 fetchAkademskaGodina(){
    this.httpKlijent.get(MojConfig.adresa_servera+'/AkademskeGodine/GetAll_ForCmb').subscribe(res=>{
      this.akademskaGodina=res;
      console.log(this.akademskaGodina)
    })
 }

  fetchStudent(s:number){
    let params=new HttpParams().set("id",this.studentId.id)
    this.httpKlijent.get(MojConfig.adresa_servera+'/Student/GetStudentById',{params}).subscribe(res=>{
      this.studentPodatci=res;
      console.log(this.studentPodatci)
    })
  }
  controlModal(){
    this.hideNoviSemestar=!this.hideNoviSemestar;
  }
 upisSemestra(){
    let body={
     datumUpisa: this.datumUpisa,
     godinaStudija: this.godinaStudija||0,
     cijenaSkolarine: this.cijenaSkolarine||0,
     obnova:this.obnova,
     akademskaGodinaId: this.selectedAkademskaGodina,
     studentId: this.studentPodatci.id,
     evidentirao: AutentifikacijaHelper.getLoginInfo().autentifikacijaToken.korisnickiNalog.korisnickoIme
    }
    this.httpKlijent.post(MojConfig.adresa_servera+'/Maticna/AddMaticna',body).subscribe(res=>{
      porukaSuccess("Uspjesno upisan semestar");
     this.dohvatiSemestre();
    })
 }
 dohvatiSemestre(){

    let params=new HttpParams().set("studentId",this.studentId.id);
    this.httpKlijent.get(MojConfig.adresa_servera+'/Maticna/GetAllMaticna',{params}).subscribe(res=>{
      this.maticnaKnjigaPodatci=res;
      console.log(this.maticnaKnjigaPodatci)
    })
 }

  ngOnInit(): void {
    this.route.params.subscribe(x=>{
      this.studentId=x;

    })
    this.fetchStudent(this.studentId.id);
    this.fetchAkademskaGodina();
    this.dohvatiSemestre()
  }


  odaberiAkademskuGodinu(a: any) {
    this.selectedAkademskaGodina=a.id;
  }

  controlOvjeraModal(m?:any) {
    this.hideOvjeraSemestar=!this.hideOvjeraSemestar;
    this.napomena=''
    this.datumOvjere=new Date().toISOString().split('T')[0]
    this.selectedMaticna=m;
  }

  ovjeriSemestar() {
    let body={
      id:this.selectedMaticna.id,
      datumOvjere:this.datumOvjere,
      napomena:this.napomena
    }

    this.httpKlijent.put(MojConfig.adresa_servera+'/Maticna/EditMaticna',body).subscribe(res=>{
      porukaSuccess("Uspjesno dodati podatci");
      this.dohvatiSemestre();
    })
  }
}
