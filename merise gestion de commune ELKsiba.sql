create database BDGestionCommuneELKsiba
use BDGestionCommuneELKsiba

--Création des tables 
create table Stagiaire(IdStagiaire int primary key identity(1,1),CIN varchar(50),NomStg varchar(50),PrenomStg varchar(50),GenreStg varchar(10) check(GenreStg in('Femme','Homme')),DateNaiss Date,Saison varchar(10),DateDebut Date,DateFin Date);
create table Employé(Matr int primary key,NomEmp varchar(50),PrenomEmp varchar(50),GenreEmp varchar(10) check(GenreEmp in('Femme','Homme')),NumCompte varchar(30));
create table Encadrer(IdStagiaire int references Stagiaire(IdStagiaire) on delete cascade on update cascade ,Matr int references Employé(Matr) on delete cascade on update cascade,primary key(IdStagiaire,Matr));

--IdStagiaire,CIN,NomStg,PrenomStg,GenreStg,DateNaiss,Saison,DateDebut,DateFin  
--Matr, NomEmp,PrenomEmp,GenreEmp,NumCompte
--#IdStagiaire, #Matr
select * from Stagiaire 
select * from Employé 
select * from Encadrer 

alter table Stagiaire
add constraint ck1 check(DateFin>DateDebut)


insert into Stagiaire values('AA152','NomStg1','PreStg1','Homme','05/25/2000','2019/2020','01/01/2020','01/31/2020')
insert into Stagiaire values('Q4521','NomStg2','PreStg2','Homme','03/16/2001','2019/2020','01/01/2020','01/31/2020')
insert into Stagiaire values('1522','AB','CD','Femme','01/01/2001','2020/2021','12/20/2020','01/31/2021')
insert into Stagiaire values('IA1525','gggg','nnn','Homme','01/01/2001','2020/2021','01/01/2020','01/31/2021')

insert into Employé values(45455,'NomEmp1','PreEmp1','Homme','4541321521')
insert into Employé values(554,'NomEmp2','PreEmp3','Homme','1111111011111056')
insert into Employé values(854,'NomEmp3','PreEmp3','Femme','0000000002152')

insert into Encadrer values(10,45455)
insert into Encadrer values(11,45455)
insert into Encadrer values(11,854)
insert into Encadrer values(12,45455)
insert into Encadrer values(10,854)
insert into Encadrer values(11,554)

select Employé.Matr as 'Matricule',CIN,Stagiaire.IdStagiaire,NomEmp+' '+PrenomEmp as 'Encadré par',NomStg+' '+PrenomStg as 'Stagiaire',DateDebut as'Date Debut',DateFin from Stagiaire left join Encadrer on Stagiaire.IdStagiaire=Encadrer.IdStagiaire
                                           left join Employé on Employé.Matr=Encadrer.Matr where DateDebut<=GETDATE() and DateFin>=GETDATE();
select * from Encadrer
delete from Stagiaire

select Encadrer.IdStagiaire,NomStg+' '+PrenomStg as 'Stagiaire',Encadrer.Matr,NomEmp+' '+PrenomEmp 'Encadré par',DateDebut as 'Date Début',DateFin 'Date Fin'
from Encadrer inner join Employé on Encadrer.Matr=Employé.Matr
              inner join Stagiaire on Encadrer.IdStagiaire=Stagiaire.IdStagiaire


select Encadrer.IdStagiaire,NomStg+' '+PrenomStg as 'Stagiaire',Encadrer.Matr,NomEmp+' '+PrenomEmp 'Encadré par',DateDebut as 'Date Début',DateFin 'Date Fin'
from Encadrer inner join Employé on Encadrer.Matr=Employé.Matr
              inner join Stagiaire on Encadrer.IdStagiaire=Stagiaire.IdStagiaire
where DateDebut>='11/20/2020' and DateFin<='02/28/2021'

select IdStagiaire 'ID',CIN,NomStg+' '+PrenomStg 'Nom et prenom',DateDebut 'Date Début',DateFin 'Date Fin' from Stagiaire
