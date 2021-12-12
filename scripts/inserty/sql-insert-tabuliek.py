from faker import Faker
from random import randint, uniform


def create_insert_to_file(file_name):
    lines = list()

    names = ['AstraZeneca', 'Pfizer', 'Novartis', 'Beecham Group', 'GlaxoSmithKline']
    id = 0
    id_man = ['111222', '333444', '555666', '777888', '999111']
    count_manufacturer = len(names)
    for name in names:
        fake = Faker(['en-GB'])
        values = '\'' + id_man[id] + '\', \'' + name + '\', \'' + fake.address().replace('\n', ' , ') + '\''
        insert = "insert into manufacturer(id_man, name, address) values ({0});".format(values)
        lines.append(insert)
        id += 1

    roles = ['admin', 'user']
    for role in roles:
        insert = "insert into person_role(role_name) values (\'{0}\');".format(role)
        lines.append(insert)

    user_names = ['admin', 'cajka', 'novakova', 'novotnak', 'grofcik']
    names = ['Admin', 'Čajka', 'Nováková', 'Novotňák', 'Grofčík']
    # admin, cajka, novakova, novotnak, grofcik
    psw = ['21232f297a57a5a743894a0e4a801fc3', '38a5018600e967dbd2deedc90b296fc9', 'fc77b0402f5cd373af729838bafdfd9d', '1de701b85387fd0a6d3a38919a92744e', '4052f667f263cd696f44009926b15c07']
    count_person = len(names)
    id = 0
    for name in user_names:
        if name == 'admin':
            values = '1' + ', \'' + name + '\', \'' + psw[id] + '\',\'' + names[id] + '\''
        else:
            values = '2' + ', \'' + name + '\', \'' + psw[id] + '\',\'' + names[id] + '\''
        id += 1
        insert = "insert into person(id_role, user_name, password, name) values ({0});".format(values)
        lines.append(insert)

    count_sales_history = 0
    for i in range(3000):
        values = str(randint(1, count_person)) + ', ADD_MONTHS(SYSDATE, -(12 * ' + str(randint(0, 40)) + ')), ' + str(round( uniform(1000, 10000), 2))
        insert = "insert into sales_history(id_user, sales_date, price) values ({0});".format(values)
        lines.append(insert)
        count_sales_history += 1

    inn = [0, 24, 25, 27]
    count_prescription = 0
    for i in range(650):
        values = '\'' + str(randint(50, 99)) + '01' + str(randint(1, 31)) + '\' , ADD_MONTHS(SYSDATE, -' + str(randint(0, 450)) + '), ' + str(randint(3000, 3500)) + ', ' + str(inn[randint(0, 3)])
        insert = "insert into e_prescription(rc_patient, prescription_date, code_doctor, insurrance_num) values ({0});".format(values)
        lines.append(insert)
        count_prescription += 1

    names = ['PANADOL Novum', 'PANADOL Extra Novum', 'Acylpyrin', 'Paralen', 'Dorithricin', 'Celaskon', 'Ibalgin',
             'Zinkorot', 'ASPIRIN', 'Brufen', 'Voltaren', 'Nalgesin', 'NUROFEN', 'MEDIPYRIN', 'VALETOL', 'RENNIE', 'ISOCHOL',
             'IMODIUM', 'PANZYNORM', 'Pancreolan', 'No-SPA', 'NOLPAZA', 'KINEDRYL', 'LOPACUT', 'CILKANOL', 'WOBENZYM',
             'MILGAMMA', 'DEVENAL', 'VITANGO', 'VENORUTON', 'CANEPHRON', 'MAGNEROT', 'ARTERIN', 'COLAFIT', 'MAGNE B6',
             'REVALID', 'JAMIESON D3', 'JAMIESON Zinok', 'CLINICAL Melatonin', 'Polikosanol', 'Mamavit', 'PERSEN Forte',
             'PERSEN', 'VITANGO', 'NALGESIN S', 'PANADOL Migréna', 'CETALGEN', 'VOLTAREN Rapid', 'Ascorutin', 'ZODAC']
    count_med = 0
    for i in range(20000):
        medicament = names[randint(0, len(names)-1)]
        values = '\'' + id_man[randint(0, count_manufacturer - 1)] + '\', \'' + medicament + ' -' + str(count_med) + '\', \'' + '0' + '\', med_type(\'tablety\') '
        insert = "insert into medicament(id_man, name, is_prescribed, type) values ({0});".format(values)
        lines.append(insert)
        count_med += 1

    names = ['ANRETHAL Respirátor', 'Dávkovač liekov', 'Skúmavka na moč', 'RENMED Rúško', 'FFP2 Detský respirátor', 'Ovulačný test', 'Tehotenský test',
             'Ovínadlo elastické fixačné', 'Chránič sluchu 1 pár', 'Prúžky EasyTouch', 'Bandáž kolena', 'Bandáž na zápästie',
             'Obličkový obojstranný pás', 'THERMO náplasť hrejivá', 'THERMO náplasť chladivá', 'Striekačka',
             'Rektálna trubička', 'PERSEN Forte', 'Rúško chirurgické jednorázové', 'Teplomer', 'Rukavice']
    for i in range(5000):
        medicament = names[randint(0, len(names) - 1)]
        values = '\'' + id_man[randint(0, count_manufacturer - 1)] + '\', \'' + medicament + ' -' + str(count_med) + '\', \'' + '0' + '\', med_type(\'zdravotné potreby\') '
        insert = "insert into medicament(id_man, name, is_prescribed, type) values ({0});".format(values)
        lines.append(insert)
        count_med += 1

    names = ['FAKTU masť', 'FLECTOR EP', 'IMAZOL Plus', 'HEPAROID', 'HYDROCORTISON', 'VERAL gél', 'DOLGIT gél',
             'IBALGIN gél', 'IBALGIN krém', 'VOLTAREN gél', 'FENISTIL gél', 'REPARIL', 'CORNEREGEL očný gél',
             'KAMISTAD orálny gél', 'DENTINOL gél', 'GALVEX Tekutý púder', 'CORSODYL gél', 'ALMIRAL gél', 'MUNDISAL gél',
             'AVENOC masť', 'LIOTON gél', 'DOLGIT masť', 'AULIN gél', 'Octanový krém']
    for i in range(10000):
        medicament = names[randint(0, len(names) - 1)]
        values = '\'' + id_man[randint(0, count_manufacturer - 1)] + '\', \'' + medicament + ' -' + str(count_med) + '\', \'' + '0' + '\', med_type(\'masť\') '
        insert = "insert into medicament(id_man, name, is_prescribed, type) values ({0});".format(values)
        lines.append(insert)
        count_med += 1

    names = ['RELPAX', 'MEDOCRIPTINE', 'MALARONE', 'FAMOSAN', 'ULCERAN', 'SOLIAN', 'RISPEN',
             'NOLPAZA ', 'OPRAZOLE', 'Rupafin', 'ALERID', 'CINIE', 'TELFAST', 'Topepsil',
             'MONOTAB 20', 'GOPTEN', 'LOKREN', 'GRIMODIN', 'Vasopentol',
             'Betaloc ZOK', 'CRESTOR', 'DIROTON PLUS', 'Karbicombi', 'BRUFEN 600']
    unprescribed_count = count_med
    for i in range(10000):
        medicament = names[randint(0, len(names) - 1)]
        values = '\'' + id_man[randint(0, count_manufacturer - 1)] + '\', \'' + medicament + ' -' + str(count_med) + '\', \'' + '1' + '\', med_type(\'tablety\') '
        insert = "insert into medicament(id_man, name, is_prescribed, type) values ({0});".format(values)
        lines.append(insert)
        count_med += 1

    for i in range(1, count_med+1):
        if 20000 < i < 25001:
            values = str(i) + "," + str(randint(1, 100))
            insert = "insert into storage(id_med, quantity) values ({0});".format(values)
        else:
            values = str(i) + "," + str(randint(50, 500)) + ', ADD_MONTHS(SYSDATE,' + str(randint(3, 36)) + ')'
            insert = "insert into storage(id_med, quantity, expiration_date) values ({0});".format(values)
        lines.append(insert)

    for i in range(1, count_med+1):
        values = str(i) + ', ' + str(round(uniform(0.30, 30), 2)) + ', \'0\', ADD_MONTHS(SYSDATE, -(12 * 50))'
        insert = "insert into price_list(id_med, price, discount, date_from) values ({0});".format(values)
        lines.append(insert)

    id_pres = 0
    id_med = unprescribed_count + 1
    for i in range(1000):
        if i % 10 == 0:
            id_pres += 1
        values = str(id_pres) + ', ' + str(id_med) + ', ' + str(randint(1, count_sales_history)) + ',1'
        insert = "insert into sale_item(id_pres, id_med, id_sale, quantity) values ({0});".format(values)
        id_med += 1
        lines.append(insert)

    with open(file_name, 'w') as f:
        for line in lines:
            f.write(line + '\n')


if __name__ == '__main__':
    create_insert_to_file("sql-inserty1.txt")

