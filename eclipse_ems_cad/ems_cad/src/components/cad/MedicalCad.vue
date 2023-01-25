<template>
    <section class="medic_cad fullpage">
        <transition name="pad">
            <div class="pad_outer" v-show="show">
                <div class="pad_inner">
                    <div class="pad">
                        <transition name="transition_03">
                            <div class="page page_0" v-show="logged == 0">
                                <div class="logo"></div>
                                <h2 v-text="$ml.get('cad_name')">SAN-ANDREAS CAD</h2>
                                <button class="btn btn-login" @click="login" v-text="$ml.get('button_enter')">
                                    Вход
                                </button>
                                <h5 v-text="$ml.get('cad_slogan')">TO PROTECT AND TO SERVE</h5>
                            </div>
                        </transition>

                        <div class="page page_loader">
                            <div class="loading"> 
                                <svg width="16px" height="12px">
                                    <polyline id="back" points="1 6 4 6 6 11 10 1 12 6 15 6"></polyline>
                                    <polyline id="front" points="1 6 4 6 6 11 10 1 12 6 15 6"></polyline>
                                </svg>
                            </div>
                        </div>

                        <transition name="transition_03">
                            <div class="page page_1" v-show="logged == 2">
                                <div class="top_bar">
                                    <div class="row">
                                        <div class="col-2 d-flex justify-content-start align-items-center">
                                            <div class="logo"></div>
                                        </div>
                                        <div class="col-8 d-flex justify-content-center align-items-center">
                                            <h2 v-text="$ml.get('cad_fullname')">Computer-Aided Dispatching</h2>
                                        </div>
                                        <div class="col-2 d-flex justify-content-end align-items-center">
                                            <button class="btn btn-gold btn-close" @click="closeCad" v-text="$ml.get('button_exit')">Выход</button>
                                        </div>
                                    </div>
                                    <ul class="list-group">
                                        <li class="list-group-item" :class="{active: activeMenu == 0}" @click="selectMenu(0)" v-text="$ml.get('main_button_1')">Основная панель</li>
                                        <li class="list-group-item" :class="{active: activeMenu == 1}" @click="selectMenu(1)" v-text="$ml.get('main_button_2')">Вызовы</li>
                                        <li class="list-group-item" :class="{active: activeMenu == 2}" @click="selectMenu(2)" v-text="$ml.get('main_button_3')">База жителей</li>
                                        <li class="list-group-item" :class="{active: activeMenu == 3}" @click="selectMenu(3)" v-text="$ml.get('main_button_4')">База автомобилей</li>
                                        <li class="list-group-item" :class="{active: activeMenu == 4}" @click="selectMenu(4)" v-text="$ml.get('main_button_5')">Законы штата</li>
                                        <li class="list-group-item" :class="{active: activeMenu == 6}" @click="selectMenu(6)" v-text="$ml.get('main_button_7')" v-if="this.medicData.rank >= rankAdmin">Управление</li>
                                    </ul>
                                </div>
                                <div class="content">
                                    <div class="center_block">
                                        <transition name="list">
                                            <div class="c_page c_page_1 scrolled" v-show="page == 0">
                                                <div class="d-flex justify-content-between" >
                                                    <h2 v-text="$ml.get('main_button_1')">Основная панель</h2>
                                                </div>

                                                <div class="onduty_block_outer width100" v-show="onPanic()">
                                                    <h5 class="redButton" v-text="$ml.get('p1_title_0')">Активный красный код</h5>
                                                    <div class="onduty_block">
                                                        <div class="onduty_list">
                                                            <div class="onduty_element" v-for="(item, index) in medicDutyList" :key="'panic'+index" v-show="item.onPanic">
                                                                <span class="callsign">{{item.callsign}}</span>
                                                                {{ item.name }}
                                                                <i class="callsign" v-if="item.rank" :class="rankList[item.rank].shortname"></i>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="med_row">
                                                    <div class="onduty_block_outer">
                                                        <h5 v-text="$ml.get('p1_title_1')">Сотрудники на смене</h5>
                                                        <div class="onduty_block">
                                                            <div class="onduty_list">
                                                                <div class="onduty_element" v-for="(item, index) in showOnDuty(true)" :key="'activeDuty_' + index">
                                                                    <span class="callsign">{{item.callsign}}</span>
                                                                    {{ item.name }}
                                                                    <i :class="'status_' + item.onDuty"></i>
                                                                </div>
                                                            </div>
                                                            <div class="onduty_list">
                                                                <div class="onduty_element inactive" v-for="(item, index) in showOnDuty(false)" :key="'inactiveDuty_' + index">
                                                                    <span class="callsign">{{item.callsign}}</span>{{ item.name }}
                                                                    <i :class="'status_' + item.onDuty"></i>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="wanted_block_outer">
                                                        <div class="wanted_block">
                                                            <h5 v-text="$ml.get('p1_title_3')">Список больных</h5>
                                                            <div class="wanted_list person">
                                                                <div class="wanted_element" v-for="(item, index) in showPatients()" :key="'wanted_person' + index" @click="showFullContact(findPlayerIndexById(item.id))">
                                                                    <span>{{item.name}} <span :class="'patient_'+item.status">{{playerStatusNames[item.status]}}</span></span>
                                                                    <span>{{item.phone}}</span>
                                                                </div>
                                                                <div v-if="showPatients().length == 0" v-text="$ml.get('p1_zero')"></div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                               
                                            </div>
                                        </transition>

                                        <transition name="list">
                                            <div class="c_page c_page_1 scrolled" v-show="page == 1">
                                                <div class="d-flex justify-content-between">
                                                    <h2 v-text="$ml.get('main_button_2')">Последние вызовы</h2>
                                                    <input v-model="searchIdInput" @input="searchId" class="form-control search" :placeholder="$ml.get('text_search')" />
                                                </div>
                                                <div class="police_call_list">
                                                    <div :class="[{active: onScene(item.crew) == true}, 'medic_call_item status_' + item.status]" v-for="(item, index) in medicCallsList" :key="'calls_' + item.id" v-show="item.show" @click="showFullPoliceCall(index)">
                                                        <div class="row">
                                                            <div class="col-3">
                                                                <h6>{{getDate(item.date)}}</h6>
                                                                <h6 :class="'status status_' + item.status">({{item.status}}) {{getStatus(item.status)}}</h6>
                                                            </div>
                                                            <div class="col-6">
                                                                <h6><span v-text="$ml.get('text_call')">Вызов</span> №{{item.id}}</h6>
                                                                <p>{{item.message}}</p>
                                                            </div>
                                                            <div class="col-3">
                                                                <h6 class="text-right" v-text="$ml.get('text_scene')">На сцене</h6>
                                                                <div class="d-flex justify-content-end" v-if="item.crew">
                                                                    <span class="unit" :class="{active:unit == medicData.medicId,}" v-for="(unit, index) in item.crew" :key="'crew' + index">
                                                                        {{getCallsignById(unit)}}
                                                                    </span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </transition>

                                        <transition name="list">
                                            <div
                                                class="c_page c_page_11"
                                                v-show="page == 11">
                                                <div class="d-flex justify-content-between">
                                                    <button class="btn btn-gold" @click="selectPage(1)" v-text="$ml.get('button_back')">Назад</button>
                                                    <h2 class="mb-0"><span v-text="$ml.get('text_call')">Вызов</span> №{{activeCall.id}}</h2>
                                                </div>
                                                <div :class="'medic_call_item_description status_' + activeCall.status">
                                                    <p>{{activeCall.message}}</p>
                                                    <h6><span v-text="$ml.get('text_called')">Вызывавший</span>: <span class="golden">{{activeCall.from}}</span></h6>
                                                    <div class="d-flex justify-content-between">
                                                        <h6><span v-text="$ml.get('text_units')">Юниты на сцене</span>: <span class="unit" :class="{active: unit == medicData.medicId}" v-for="(unit, index) in activeCall.crew" :key="'scene' + index">{{unit}} {{getCallsignById(unit)}}</span>
                                                        </h6>
                                                        <div class="dropdown">
                                                            <a :class="'btn btn-dropdown dropdown-toggle status_' + activeCall.status" @click="showDropDown(1)">({{activeCall.status}}) {{statusNames[activeCall.status]}}</a>
                                                            <div class="dropdown-menu" v-show="showDropdownA">
                                                                <a :class="'dropdown-item status_' + index" v-for="(item, index) in statusNames" :key="'units' + index" @click="changeStatus(index)">({{index}}) {{ item }}</a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="medic_call_item_buttons">
                                                    <button class="btn btn-gold" :class="{active: onScene( activeCall.crew)}" @click="leaveScene(activeCall.crew)">{{onScene(activeCall.crew)? $ml.get('button_leave'): $ml.get('button_accept')}}</button>
                                                    <button v-text="$ml.get('button_map')" class="btn btn-gold" @click="setMark(activeCall.id)" >Показать на карте</button>
                                                </div>
                                            </div>
                                        </transition>

                                        <transition name="list">
                                            <div class="c_page c_page_2 scrolled" v-show="page == 2">
                                                <div class="d-flex justify-content-between">
                                                    <h2 v-text="$ml.get('main_button_3')">База жителей штата</h2>
                                                    <input v-model="searchInput" @input="searchName" class="form-control search" :placeholder="$ml.get('text_search')" />
                                                </div>
                                                <div class="player_list">
                                                    <div :class="'medic_call_item status_' + item.status" v-for="(item, index) in playersList" :key="'searchA' + item.id" v-show="item.show" @click="showFullContact(index)">
                                                        <div class="d-flex">
                                                            <div class="item_index">
                                                                {{ index }}
                                                            </div>
                                                            <div class="item_name">
                                                                {{ item.name }}
                                                            </div>
                                                            <div class="item_phone">
                                                                {{ item.phone }}
                                                            </div>
                                                            <div class="item_date">
                                                                {{getDate(item.registered)}}
                                                            </div>
                                                            <div :class="'item_status status_' + item.status">
                                                                {{getPlayerStatus2(item.status)}}
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </transition>

                                        <transition name="list">
                                            <div class="c_page c_page_2 profile scrolled" v-if="page == 21">
                                                <div class="d-flex justify-content-between">
                                                    <button class="btn btn-gold" @click="selectPage(2)" v-text="$ml.get('button_back')">Назад</button>
                                                    <h2 class="mb-0">{{activePlayer.name}}</h2>
                                                </div>
                                                <div class="row">
                                                    <div class="col-4">
                                                       <div class="profile_image">
                                                           <button class="btn btn-gold upload" @click="showModalC = true">Upload photo</button>
                                                            <img class="img-fluid" v-if="activePlayer.picture" :src="activePlayer.picture" />
                                                        </div>
                                                    </div>
                                                    <div class="col-8 pl-0">
                                                        <div :class="'profile_description status_' + activePlayer.status">
                                                            <div class="d-flex justify-content-between">
                                                                <h5 v-text="$ml.get('p3_birthdate')">Дата рождения:</h5>
                                                                <h5>{{activePlayer.age}}
                                                                </h5>
                                                            </div>
                                                            <div class="d-flex justify-content-between">
                                                                <h5 v-text="$ml.get('p3_gender')">Пол:</h5>
                                                                <h5>{{activePlayer.gender}}
                                                                </h5>
                                                            </div>
                                                            <div class="d-none justify-content-between">
                                                                <h5>Вод. лицензия:</h5>
                                                                <h5 :class="{license_false: !activePlayer.driveLicense}">{{checkLicense(activePlayer.driveLicense)}}</h5>
                                                            </div>
                                                            <div class="d-flex justify-content-between">
                                                                <h5 v-text="$ml.get('p3_phone')">Телефон:</h5>
                                                                <h5>{{activePlayer.phone}}</h5>
                                                            </div>
                                                            <div class="d-flex justify-content-between">
                                                                <h5 class="mb-0" v-text="$ml.get('p3_database')">Запись в БД:</h5>
                                                                <h5>{{activePlayer.id}}</h5>
                                                            </div>
                                                            <div class="d-flex justify-content-between">
                                                                <h5 class="mb-0" v-text="$ml.get('p3_insurance')"></h5>
                                                                <h5 v-if="activePlayer.insurance == true" v-text="$ml.get('text_true')"></h5>
                                                                <h5 v-else v-text="$ml.get('text_false')"></h5>
                                                            </div>
                                                            <div class="d-flex justify-content-between">
                                                                <h5 class="mb-0" v-text="$ml.get('p3_status')">Статус:</h5>
                                                                <div class="dropdown">
                                                                    <a :class="'btn btn-dropdown dropdown-toggle status_' + activePlayer.status" @click="showDropDown(2)">({{activePlayer.status}}) {{playerStatusNames[activePlayer.status]}}</a>
                                                                    <div class="dropdown-menu" v-show="showDropdownB">
                                                                        <a :class="'dropdown-item status_' + index" v-for="(item, index) in playerStatusNames" :key="'searchB' + index" @click="changePlayerStatus(index)">({{index}}) {{item}}</a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="d-none profile_content">
                                                    <p><b>Место жительства:</b>{{activePlayer.address}}</p>
                                                    <p><b>Комментарий:</b>{{activePlayer.about}}</p>
                                                </div>

                                                <div class="ticket_list" v-if="activeTickets.length != 0">
                                                    <h4 v-text="$ml.get('p3_tickets')">Штрафы гражданина</h4>
                                                    <div class="ticket_list_inner">
                                                       <div class="ticket header">
                                                            
                                                            <div class="item_ticket_id">ID</div>
                                                            <div class="item_ticket_owner" v-text="$ml.get('p3_author')">Выписал</div>
                                                            <div class="item_ticket_date" v-text="$ml.get('p3_date')">Дата</div>
                                                            <div class="item_ticket_text" v-text="$ml.get('p3_reason')">Причина штрафа</div>
                                                            <div class="item_ticket_price" v-text="$ml.get('p3_amount')">Сумма</div>
                                                            <div class="item_ticket_index" v-text="$ml.get('p3_time')">Срок</div>
                                                            <div class="item_ticket_paid" v-text="$ml.get('p3_paid')">Paid</div>
                                                            <div class="item_ticket_delete"></div>
                                                        </div>
                                                       <div class="ticket" v-for="(ticket, newindex) in activeTickets" :key="'ticket' + newindex + ticket.id">
                                                            
                                                            <div class="item_ticket_id">
                                                                {{ ticket.id }}
                                                            </div>
                                                            <div class="item_ticket_owner">
                                                                {{getOwnerId(ticket.operatorId)}}
                                                            </div>
                                                            <div class="item_ticket_date">
                                                                {{getDate(ticket.date)}}
                                                            </div>
                                                            <div class="item_ticket_text">
                                                                {{ticket.text}}
                                                            </div>
                                                            <div class="item_ticket_price">
                                                                {{ticket.price}}
                                                            </div>
                                                            <div class="item_ticket_index">
                                                                {{ticket.penalty}}
                                                            </div>
                                                            <div :class="{false: !ticket.paid}" class="item_ticket_paid">
                                                                {{checkPaid(ticket.paid)}}
                                                            </div>
                                                            <div class="item_ticket_delete">
                                                                <button class="del" @click="deleteTicket(newindex)"></button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="police_tickets">
                                                    <div class="d-flex justify-content-end">
                                                        <button class="btn btn-gold mr-2" v-if="activePlayer.insurance == false || !activePlayer.insurance" @click="addInsuranceToPlayer()" v-text="$ml.get('p3_add_insurance')"></button>
                                                        <button class="btn btn-gold mr-2" v-if="activePlayer.insurance == true" @click="removeInsuranceFromPlayer()" v-text="$ml.get('p3_remove_insurance')"></button>
                                                        <button class="btn btn-gold" @click="addTicketToActivePlayer()" v-text="$ml.get('p3_ticket')"></button>
                                                    </div>
                                                </div>
                                            </div>
                                        </transition>

                                        <transition name="list">
                                            <div class="c_page c_page_2 scrolled" v-show="page == 3">
                                                <div class="d-flex justify-content-between">
                                                    <h2 v-text="$ml.get('main_button_4')"></h2>
                                                    <input v-model="searchVehicleInput" @input="searchVehicle" class="form-control search" :placeholder="$ml.get('text_search')" />
                                                </div>
                                                <div class="vehicles_list">
                                                    <div class="d-flex law_header">
                                                        <div style="width: 5%">ID</div>
                                                        <div style="width: 10%" v-text="$ml.get('p3_date')">Дата</div>
                                                        <div style="width: 16%" v-text="$ml.get('p3_author')">Выписал</div>
                                                        <div style="width: 16%" v-text="$ml.get('p3_patient')">Выписал</div>
                                                        
                                                        <div style="width: 45%" v-text="$ml.get('p3_reason')">Причина штрафа</div>
                                                        <div style="width: 4%" v-text="$ml.get('p3_amount')">Сумма</div>
                                                        <div style="width: 4%" v-text="$ml.get('p3_paid')" class="text-right">Paid</div>
                                                    </div>
                                                    <div class="medic_call_item" v-for="(item, index) in ticketsList" :key="'ticketlist' + index + item.id" v-show="item.show">
                                                        <div class="d-flex">
                                                            <div class="item_id">
                                                                {{item.id}}
                                                            </div>
                                                            <div class="item_date">
                                                                {{getDate(item.date)}}
                                                            </div>
                                                            <div class="item_operator">
                                                                {{getCallsignById(item.operatorId)}}
                                                            </div>
                                                            <div class="item_owner">
                                                                {{getOwnerId(item.ownerId)}}
                                                            </div>
                                                            
                                                            <div class="item_text">
                                                                {{item.text}}
                                                            </div>
                                                            <div class="item_price">
                                                                {{item.price}}
                                                            </div>
                                                            <div class="item_paid">
                                                                {{item.paid}}
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </transition>

                                        <transition name="list">
                                            <div class="c_page c_page_3 scrolled" v-show="page == 4">
                                                <div class="d-flex justify-content-between">
                                                    <h2 v-text="$ml.get('main_button_5')"></h2>
                                                    <input v-model="searchLawsInput" @input="searchLaws" class="form-control search" :placeholder="$ml.get('text_search')" />
                                                </div>
                                                <div class="d-flex law_header">
                                                    <div style="width: 10%">№</div>
                                                    <div style="width: 70%" v-text="$ml.get('p5_text')">Текст</div>
                                                    <div style="width: 20%" class="text-right" v-text="$ml.get('p5_penalty')">Штраф</div>
                                                </div>

                                                <div class="law_list" v-for="(item, index) in allLaws" :key="'laws' + index" v-show="item.show">
                                                    <div style="width: 10%">
                                                        {{item.code}}
                                                    </div>
                                                    <div style="width: 70%">
                                                        <p>{{item.name}}</p>
                                                    </div>
                                                    <div style="width: 20%" class="text-right">
                                                        {{item.price}}$
                                                    </div>
                                                </div>
                                            </div>
                                        </transition>

                                        <transition name="list">
                                            <div class="c_page c_page_3 scrolled" v-show="page == 5">
                                                <div class="d-flex justify-content-between">
                                                    <h2 v-text="$ml.get('main_button_6')">10-коды</h2>
                                                    <input v-model="search10Input" @input="searchPoliceCodes" class="form-control search" :placeholder="$ml.get('text_search')" />
                                                </div>
                                                <p v-for="(item, index) in policeCodes" :key="'codes' + index" v-show="item.show">{{ item.text }}</p>
                                            </div>
                                        </transition>

                                        <transition name="list">
                                            <div class="c_page c_page_2 admin_police scrolled" v-show="page == 6">
                                                <div class="d-flex justify-content-between">
                                                    <h2 v-text="$ml.get('main_button_7')">Управление</h2>
                                                </div>
                                                <div class="player_list">
                                                    <div class="medic_call_item" v-for="(item, index) in medicDutyList" :key="'edit_cop_' + index" @click="showPoliceFullPage(index)">
                                                       <div class="d-flex admin_police_item">
                                                            <div class="numb">
                                                                {{index}}
                                                            </div>
                                                            <div class="name">
                                                                {{item.name}}
                                                            </div>
                                                            <div class="callsign">
                                                                {{item.callsign}}
                                                            </div>
                                                            <div class="rank">
                                                                {{rankList[item.rank].name}}
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </transition>

                                        <transition name="list">
                                            <div class="police-modal" v-if="showModalA">
                                                <div class="modal_inner">
                                                    <div class="d-flex justify-content-between">
                                                        <h2 v-text="$ml.get('p3_ticket')">Выписка штрафа</h2>
                                                        <button class="btn btn-gold" @click="(showModalA = false), clearViolationList()" v-text="$ml.get('p3_close')">Закрыть</button>
                                                    </div>
                                                    <div class="modal_content">
                                                        <h5><span v-text="$ml.get('p3_officer')">Имя офицера: </span>{{playerData.name}}</h5>
                                                        <h5><span v-text="$ml.get('p3_name')">Нарушитель: </span>{{activePlayer.name}}</h5>
                                                        <h5><span v-text="$ml.get('p3_date2')">Дата: </span>{{ todayDate() }}</h5>
                                                        <h4 v-text="$ml.get('p3_violations')">Список нарушений:</h4>
                                                        <div class="violation_list added scrolled">
                                                            <div class="violation_item" v-for="(violation,index) in activeViolations" :key="'addedviolation' + index">
                                                                <div class="violation_item_code">
                                                                    {{violation.code}}
                                                                </div>
                                                                <div class="violation_item_name">
                                                                    {{violation.name}}
                                                                </div>
                                                                <div class="violation_item_price">
                                                                    {{violation.price}}
                                                                </div>
                                                                <div class="violation_item_button">
                                                                    <a @click="deleteViolationFromList(index)" v-text="$ml.get('p3_delete')">Удалить</a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="violation_list scrolled">
                                                            <div class="violation_item"
                                                                v-for="(violation, index) in allLaws" :key="'violations' + index">
                                                                <div class="violation_item_code">
                                                                    {{violation.code}}
                                                                </div>
                                                                <div class="violation_item_name">
                                                                    {{violation.name}}
                                                                </div>
                                                                <div class="violation_item_price">
                                                                    ${{violation.price}}
                                                                </div>
                                                                <div class="violation_item_button">
                                                                    <a @click="addViolationToList(index)" v-text="$ml.get('p3_add')">Добавить</a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="d-flex justify-content-between align-items-center">
                                                        <h5><span v-text="$ml.get('p3_amount2')">Сумма</span>: ${{violationsSum}}<span v-show="violationsTime" class="nob">, <span v-text="$ml.get('p3_time')">срок</span>: {{violationsTime}} <span v-text="$ml.get('p3_short')">мин.</span> ({{violationsTimeConverted}} h.)</span></h5>
                                                        <button class="btn btn-gold" @click="issueFine" v-text="$ml.get('p3_write')">Выписать штраф</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </transition>

                                        <transition name="list">
                                            <div class="police-modal" v-if="showModalB">
                                                <div class="modal_inner">
                                                    <div class="d-flex justify-content-between">
                                                        <h2 v-text="$ml.get('p7_name')">Данные сотрудника</h2>
                                                        <button class="btn btn-gold" @click="(showModalB = false), (showDropdownD = false)" v-text="$ml.get('p7_cancel')">Закрыть</button>
                                                    </div>
                                                    <div class="modal_content">
                                                        <h5><span v-text="$ml.get('p7_officer')">Имя офицера:</span>{{activeCop.name}}</h5>
                                                        <div class="d-flex">
                                                            <h5><span v-text="$ml.get('p7_rank')">Звание:</span></h5>
                                                            <div class="dropdown">
                                                                <a class="btn btn-dropdown dropdown-toggle police-toggle" @click="showDropDown(4)">{{rankList[activeCop.rank].name}}
                                                                    <i class="callsign" v-if="activeCop.rank" :class="rankList[activeCop.rank].shortname"></i>
                                                                </a>
                                                                <div class="dropdown-menu" v-show="showDropdownD">
                                                                    <a class="dropdown-item" :class="{active: activeCop.rank == index}" v-for="(item, index) in rankList" :key="'rank_' + index" @click="changePlayerRank(index)">{{index}} {{item.name}}</a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="d-flex">
                                                            <h5><span v-text="$ml.get('p7_callsign')">Позывной: </span></h5>
                                                            <input class="callsign" v-model="callsignTemp">
                                                            <button class="btn btn-callsign" @click="saveNewCallsign()" v-text="$ml.get('p7_change')">Изменить</button>
                                                        </div>
                                                    </div>
                                                    <div class="d-flex justify-content-start align-items-center">
                                                        <button v-show="!sureFire" class="btn btn-danger" @click="(sureFire = true), (showDropdownD = false)" v-text="$ml.get('p7_fire')">Уволить сотрудника</button>
                                                        <h5 class="mb-0" v-show="sureFire" v-text="$ml.get('p7_sure')">Вы уверены?</h5>
                                                        <button v-show="sureFire" class="ml-3 mr-3 btn btn-danger" @click="fireCop(activeCop)" v-text="$ml.get('p7_fire')">Уволить</button>
                                                        <button v-show="sureFire" class="btn btn-gold" @click="sureFire = false" v-text="$ml.get('p7_cancel')">Отмена</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </transition>

                                        <transition name="list">
                                            <div class="police-modal" v-if="showModalC">
                                                <div class="modal_inner">
                                                    <div class="d-flex justify-content-between">
                                                        <h2 v-text="$ml.get('p8_name')">Загрузить фотографию</h2>
                                                        <button class="btn btn-gold" @click="(showModalC = false)" v-text="$ml.get('p8_close')">Закрыть</button>
                                                    </div>
                                                    <div class="modal_content d-flex flex-column align-items-start">
                                                        <label v-text="$ml.get('p8_link')">Ссылка на фотографию</label>
                                                        <input class="photo_upload" v-model="inputImage">
                                                        <button class="btn btn-green" @click="addImageToPlayer()" v-text="$ml.get('p8_save')">Save</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </transition>
                                    </div>
                                </div>

                                <div class="bottom_bar">

                                    <div class="bottom_block">
                                        <div class="onDuty_buttons">
                                            <b-button v-b-tooltip.hover.top :title="$ml.get('title_status_1')" class="btn onDuty_1" :class="{active: this.medicData.onDuty == 1}" @click="changeShift(1)"></b-button>
                                            <b-button v-b-tooltip.hover.top :title="$ml.get('title_status_2')" class="btn onDuty_2" :class="{active: this.medicData.onDuty == 2}" @click="changeShift(2)"></b-button>
                                            <b-button v-b-tooltip.hover.top :title="$ml.get('title_status_3')" class="btn onDuty_3" :class="{active: this.medicData.onDuty == 3}" @click="changeShift(3)"></b-button>
                                            <b-button v-b-tooltip.hover.top :title="$ml.get('title_status_4')" class="btn onDuty_0" :class="{active: this.medicData.onDuty == 0}" @click="changeShift(0)"></b-button>
                                        </div>

                                        <button class="btn btn-panic" :class="{ active: panicOn }" @click="panicClick()" v-text="$ml.get('main_button_panic')">Кнопка паники</button>
                                    </div>

                                    <div class="player_info">
                                        <h3>{{this.playerData.name}}</h3>
                                        <h4>{{this.rankList[this.medicData.rank].name}} ({{ this.medicData.callsign}})</h4>
                                        <button :class="['onDuty_' + this.medicData.onDuty + ' btn-shift hover-shift']"></button>
                                    </div>
                                    

                                </div>

                            </div>
                        </transition>
                    </div>
                </div>
            </div>
        </transition>
    </section>
</template>

<script>
import axios from "axios";
import police10codes from "../../json/policecodes.json";
import vehicles from "../../json/vehicles.json";
import prices from "../../json/prices.json";

export default {
    name: "MedicalCad",
    components: {},
    props: {
        playerData: Object,
    },
    data() {
        return {
            show: false,
            logged: 0,
            page: 0,
            activeMenu: 0,
            panicOn: false,
            playerOnPatrol: 'Not selected',

            policeCodes: {},
            allVehicles: vehicles,
            allLaws: prices,

            medicData: this.playerData.medicData,
            playersList: [],
            vehiclesList: [],
            ticketsList: [],
            medicCallsList: [],
            medicDutyList: [],
            medicAdamList: [],

            activePlayer: {},
            activeTickets: [],
            activeCall: {},
            activeVehicle: {},
            activeViolations: [],
            activeCop: {},
            activePanic: [],

            searchInput: "",
            searchIdInput: "",
            search10Input: "",
            searchLawsInput: "",
            searchVehicleInput: "",

            statusNames: [
                "Under control",
                "Unknown",
                "Normal",
                "Threat",
                "Danger",
            ],
            playerStatusNames: [
                "Dead",
                "Standart",
                "On treatment",
                "At the surgery",
                "Cured",
                "Special",
            ],
            vehicleStatusNames: [
                "Destroyed",
                "Standart",
                "Wanted",
                "Arrested",
                "Stolen",
                "Special",
            ],
            rankList: [
                { name: "Jr. EMT", shortname: "doc0"},
                { name: "EMT", shortname: "doc1"},
                { name: "Sr. EMT", shortname: "doc2"},
                { name: "EMT Supervisor", shortname: "doc3" },
            ],

            rankAdmin: 3,

            showDropdownA: false,
            showDropdownB: false,
            showDropdownC: false,
            showDropdownD: false,
            showModalA: false,
            showModalB: false,
            showModalC: false,

            sureFire: false,
            violationsSum: 0,
            violationsTime: 0,
            violationsTimeConverted: 0,
            callsignTemp: '',

            inputImage: undefined,

        };
    },
    methods: {
        closeCad() {
            axios.post('https://eclipse_EmsCad/CloseCad', {
            }).then(() => { return; }).catch(function () { console.log() });  
            //this.show = false;
        },
        login() {
            this.logged = 1;

            // this.playersList = [
            //     {
            //         id: 1,
            //         index: 0,
            //         isActive: true,
            //         balance: "$2,117.78",
            //         picture: "http://placehold.it/256x256",
            //         age: 28,
            //         eyeColor: "yellow",
            //         name: "Brian Jhonson",
            //         gender: "male",
            //         phone: "555-1100",
            //         address: "Police dept",
            //         about: "I am cop",
            //         registered: "Tue Jan 14 1993 11:51:18 GMT+0300",
            //         greeting: "Ggs.",
            //         driveLicense: true,
            //         status: 1,
            //     },
            //     {
            //         id: 105,
            //         index: 0,
            //         isActive: true,
            //         balance: "$2,117.78",
            //         picture: "http://placehold.it/256x256",
            //         age: 27,
            //         eyeColor: "brown",
            //         name: "Williams Sosa",
            //         gender: "male",
            //         phone: "578-2168",
            //         address: "685 Banker Street, Collins, Colorado, 9646",
            //         about:
            //             "Dolor commodo elit fugiat et do enim nulla et. Ex eiusmod labore sint quis veniam exercitation in commodo ipsum qui ea in. Minim esse anim sunt tempor nulla laboris laborum do ipsum adipisicing laborum cupidatat nisi amet. Tempor ex ea cupidatat commodo est nulla aliqua do. Cupidatat magna laboris esse qui dolor.\r\n",
            //         registered: "Tue May 04 1980 11:51:18 GMT+0300",
            //         greeting:
            //             "Hello, Williams Sosa! You have 3 unread messages.",
            //         driveLicense: true,
            //         status: 1,
            //     },
            //     {
            //         id: 106,
            //         index: 1,
            //         isActive: true,
            //         balance: "$1,773.28",
            //         picture: "http://placehold.it/256x256",
            //         age: 50,
            //         eyeColor: "brown",
            //         name: "Cathy Quinn",
            //         gender: "female",
            //         phone: "432-3097",
            //         address: "816 Middleton Street, Walland, Georgia, 8817",
            //         about:
            //             "Elit sunt nisi ad dolor velit ea veniam ad. Esse cillum excepteur consectetur eu id Lorem mollit. Nisi elit ut est in aliqua ea enim est ad esse culpa labore id duis. Ex et ad ullamco ullamco proident anim proident ut dolor ex ipsum ipsum. Id laboris ex laboris pariatur dolore qui Lorem pariatur ea non elit.\r\n",
            //         registered: "Tue May 04 1980 11:51:18 GMT+0300",
            //         greeting: "Hello, Cathy Quinn! You have 7 unread messages.",
            //         driveLicense: true,
            //         status: 4,
            //     },
            //     {
            //         id: 107,
            //         index: 2,
            //         isActive: true,
            //         balance: "$1,761.50",
            //         picture: "http://placehold.it/256x256",
            //         age: 20,
            //         eyeColor: "blue",
            //         name: "Franco Ochoa",
            //         gender: "male",
            //         phone: "438-3894",
            //         address: "880 Croton Loop, Turpin, Illinois, 4693",
            //         about:
            //             "Quis occaecat fugiat do sit ad aliqua. Eiusmod mollit nulla exercitation sint deserunt consequat voluptate est consequat occaecat deserunt proident ad. Occaecat commodo consequat exercitation esse non veniam est ipsum nulla consectetur excepteur consectetur occaecat. Irure consequat ea commodo laborum consequat Lorem et minim dolore ea mollit excepteur nisi laboris.\r\n",
            //         registered: "Tue May 04 1980 11:51:18 GMT+0300",
            //         greeting:
            //             "Hello, Franco Ochoa! You have 6 unread messages.",
            //         driveLicense: false,
            //         status: 3,
            //     },
            // ];

            this.showAdamList();
            this.sortyBycallsign(this.medicDutyList);
            this.addTicketsToPlayers();
            this.addVehiclesToPlayers();

            this.showAll(this.playersList);
            this.showAll(this.medicCallsList);
            this.showAll(this.vehiclesList);
            this.showAll(this.allLaws);

            this.sortByDate(this.medicCallsList);
            this.sortByNames(this.playersList);
            this.sortByNumbers(this.vehiclesList);

            this.convertPoliceCodes();

            setTimeout(() => {
                this.logged = 2;
            }, 2000);
        },
        selectMenu(page) {
            this.activeMenu = page;
            this.selectPage(page);

            this.showModalA = false;
            this.showModalB = false;
            this.showModalC = false;
            this.clearViolationList();
            this.closeDropDowns();
        },
        selectPage(page) {
            this.page = page;
            this.closeDropDowns();
        },
        closeDropDowns() {
            this.showDropdownA = false;
            this.showDropdownB = false;
            this.showDropdownC = false;
            this.showDropdownD = false;
        },
        getName(str) {
            var name = str.split(" ");
            return name[0];
        },
        getSurname(str) {
            var name = str.split(" ");
            return name[1];
        },
        getDate(date) {
            var newdate = new Date(date);
            if (newdate) {
                var year = newdate.getFullYear();
                var month = ("0" + (newdate.getMonth() + 1)).slice(-2);
                var day = ("0" + newdate.getDate()).slice(-2);
                return day + "." + month + "." + year;
            } else {
                newdate = "01.01.1900";
                return newdate;
            }
        },
        todayDate() {
            var today = new Date();
            var time = ("0" + today.getHours()).slice(-2);
            var minutes = ("0" + today.getMinutes()).slice(-2);
            var year = today.getFullYear();
            var month = ("0" + (today.getMonth() + 1)).slice(-2);
            var day = ("0" + today.getDate()).slice(-2);
            var date =
                time + ":" + minutes + " " + day + "." + month + "." + year;
            return date;
        },
        getStatus(numb) {
            return this.statusNames[numb];
        },
        getPlayerStatus(numb) {
            return this.playerStatusNames[numb];
        },
        getPlayerStatus2(numb) {
            if (numb <= 1) {
                return;
            } else {
                return this.playerStatusNames[numb];
            }
        },
        getVehicleStatus(numb) {
            if (numb <= 1) {
                return;
            } else {
                return this.vehicleStatusNames[numb];
            }
        },
        getOwnerName(id) {
            var player = this.playersList.find((item) => item.name == id);
            if (player) {
                return player.name;
            } else {
                var errortext = "unknown";
                return errortext;
            }
        },
        getOwnerId(id) {
            var player = this.playersList.find((item) => item.id == id);
            if (player) {
                return player.name;
            } else {
                var errortext = "unknown";
                return errortext;
            }
        },
        getOwnerPhone(id) {
            var player = this.playersList.find((item) => item.id == id);
            if (player) {
                return player.phone;
            } else {
                return;
            }
        },
        getOwnerPhoneByName(id) {
            var player = this.playersList.find((item) => item.name == id);
            if (player) {
                return player.phone;
            } else {
                return;
            }
        },
        getCallsignById(id) {
            var player = this.medicDutyList.find(
                (item) => item.medicId == id
            );
            if (player) {
                return player.callsign;
            } else {
                return;
            }
        },
        findCarByName(name) {
            if (name) {
                var car = this.allVehicles.find(
                    (item) => item.gameName == name
                );
                return car;
            }
        },
        findPlayerIndexById(id) {
            if (id != undefined) {
                return this.playersList.findIndex((item) => item.id == id);
            }
        },
        findPlayerIndexByName(name) {
            if (name != undefined) {
                return this.playersList.findIndex((item) => item.name == name);
            }
        },
        findVehicleIndexById(id) {
            if (id != undefined) {
                return this.vehiclesList.findIndex((item) => item.carId == id);
            }
        },
        addTicketsToPlayers() {
            this.playersList.forEach((player) => {
                player.tickets = this.ticketsList.filter(
                    (ticket) => ticket.ownerId == player.id
                );
            });
        },
        addVehiclesToPlayers() {
            this.playersList.forEach((player) => {
                player.cars = this.vehiclesList.filter(
                    (vehicle) => vehicle.ownerId == player.name
                );
            });
        },
        showAll(array) {
            array.forEach((element) => (element.show = true));
        },
        searchName() {
            var searchText = this.searchInput;
            if (searchText) {
                var exp = new RegExp(searchText, "igm");
                this.playersList.forEach((element) => {
                    var string =
                        element.name +
                        " " +
                        element.phone +
                        " " +
                        element.registered + 
                        " " +
                        this.getPlayerStatus2(element.status);
                    var result = string.search(exp);
                    if (result != -1) {
                        element.show = true;
                    } else {
                        element.show = false;
                    }
                });
            } else {
                this.showAll(this.playersList);
            }
        },
        searchId() {
            var searchText = this.searchIdInput;
            if (searchText) {
                var exp = new RegExp(searchText, "igm");
                this.medicCallsList.forEach((element) => {
                    var string = element.id.toString() + this.getDate(element.date) + element.from + this.getStatus(element.status);
                    var result = string.search(exp);
                    if (result == -1) {
                        element.show = false;
                    } else {
                        element.show = true;
                    }
                });
            } else {
                this.showAll(this.medicCallsList);
            }
        },
        searchPoliceCodes() {
            var searchText = this.search10Input;
            if (searchText) {
                var exp = new RegExp(searchText, "igm");
                for (var key in this.policeCodes) {
                    var element = this.policeCodes[key];
                    var string = element.text.toString();
                    var result = string.search(exp);
                    if (result == -1) {
                        element.show = false;
                    } else {
                        element.show = true;
                    }
                }
            } else {
                for (var i in this.policeCodes) {
                    this.policeCodes[i].show = true;
                }
            }
        },
        searchVehicle() {
            var searchText = this.searchVehicleInput;
            if (searchText) {
                var exp = new RegExp(searchText, "igm");
                this.ticketsList.forEach((element) => {
                    var string =
                        element.id +
                        " " +
                        element.operatorId +
                        " " +
                        this.getDate(element.date) +
                        "" +
                        element.text +
                        "" +
                        element.price +
                        "" +
                        element.paid;
                    var result = string.search(exp);
                    if (result != -1) {
                        element.show = true;
                    } else {
                        element.show = false;
                    }
                });
            } else {
                this.showAll(this.ticketsList);
            }
        },
        searchLaws() {
            var searchText = this.searchLawsInput;
            if (searchText) {
                var exp = new RegExp(searchText, "igm");
                this.allLaws.forEach((element) => {
                    var string =
                        element.code + " " + element.name + " " + element.price;
                    var result = string.search(exp);
                    if (result != -1) {
                        element.show = true;
                    } else {
                        element.show = false;
                    }
                });
            } else {
                this.showAll(this.allLaws);
            }
        },
        showFullContact(index) {
            this.activeMenu = 2;
            this.page = 21;
            this.activePlayer = this.playersList[index];
            
            if (this.activePlayer.tickets) {
                this.activeTickets = this.activePlayer.tickets;
            } else {
                this.activeTickets = []
            }
        },
        showFullVehiclePage(index) {
            this.activeVehicle = this.vehiclesList[index];
            this.activeMenu = 3;
            this.page = 31;
        },
        showFullPoliceCall(index) {
            this.activeCall = this.medicCallsList[index];
            this.page = 11;
        },
        sortByDate(array) {
            array.sort(function (a, b) {
                var dateA = new Date(a.date),
                    dateB = new Date(b.date);
                return dateB - dateA;
            });
        },
        sortByNames(array) {
            array.sort((a, b) => a.name.localeCompare(b.name));
        },
        sortyBycallsign(array) {
            array.sort((a, b) => a.callsign.localeCompare(b.callsign));
        },
        sortByNumbers(array) {
            array.sort(function (a, b) {
                return a.carId - b.carId;
            });
        },
        onScene(list) {
            if (!list) {
                return false;
            }
            var playerId = this.medicData.medicId;
            if (list.includes(playerId)) {
                return true;
            } else {
                return false;
            }
        },
        showDropDown(name) {
            if (name == 1) {
                this.showDropdownA = !this.showDropdownA;
            }
            if (name == 2) {
                this.showDropdownB = !this.showDropdownB;
            }
            if (name == 3) {
                this.showDropdownC = !this.showDropdownC;
            }
            if (name == 4) {
                this.showDropdownD = !this.showDropdownD;
            }
        },
        setMark(index) {
      axios.post('https://eclipse_EmsCad/SetMark', {
        id: this.activeCall.id

      }).then(() => { return; }).catch(function () { console.log() });
      console.log(index)
    },
    leaveScene(list) {
      var onScene = this.onScene(list);
      var playerId = this.medicData.medicId;
      if (onScene) {
        this.activeCall.crew.forEach((element, index) => {
          if (element == playerId) {
            this.activeCall.crew.splice(index, 1);
            axios.post('https://eclipse_EmsCad/Crew', {
        crew: playerId,
        id: this.activeCall.id

      }).then(() => { return; }).catch(function () { console.log() });
          }
        });
      } else {
        if (!this.activeCall.crew) {
          this.$set(this.activeCall, "crew", []);
        }
        this.activeCall.crew.push(playerId);
        axios.post('https://eclipse_EmsCad/Crew', {
        crew: playerId,
        id: this.activeCall.id

      }).then(() => { return; }).catch(function () { console.log() });
      }
      // Переключение "приехал на сцену" и "покинул сцену"
    },

    changeStatus(index) {
      this.activeCall.status = index;
      this.showDropdownA = false;
      axios.post('https://eclipse_EmsCad/ChangeCallStatus', {
        status: this.activeCall.status,
        id: this.activeCall.id

      }).then(() => { return; }).catch(function () { console.log() });
      // Смена статуса вызова
    },
    changePlayerStatus(index) {
      this.activePlayer.status = index;
      this.showDropdownB = false;
      // Смена статуса Жителя
      axios
        .post("https://eclipse_EmsCad/ChangePlayerStatus", {
          status: this.activePlayer.status,
          name: this.activePlayer.name,
        })
        .then(() => {
          return;
        })
        .catch(function() {
          console.log();
        });
    },
    changePatrolStatus(index) {
      console.log(index);
      var playerIndex = this.medicDutyList.findIndex(
        (item) => item.medicId == this.medicData.medicId
      );
      if (index == 0) {
        this.playerOnPatrol = 'Not selected';
        this.medicDutyList[playerIndex].adam = undefined;
        this.showAdamList();
      } else {
        this.medicDutyList[playerIndex].adam = index;
        this.playerOnPatrol = this.generateAdamName(index);
        this.showAdamList();
      }
      this.showDropdownC = false;
      axios
        .post("https://eclipse_EmsCad/ChangePatrolStatus", {
          medicId: this.medicDutyList[playerIndex].medicId.toString(),
          status: index,
        })
        .then(() => {
          return;
        })
        .catch(function() {
          console.log();
        });
      // Смена адам патруля
    },
    changeVehicleStatus(index) {
      this.activeVehicle.status = index;
      this.showDropdownB = false;
      axios
        .post("https://eclipse_EmsCad/ChangeVehicleStatus", {
          Status: this.activeVehicle.status,
          Number: this.activeVehicle.number,
        })
        .then(() => {
          return;
        })
        .catch(function() {
          console.log();
        });
      // Смена статуса Автомобиля
    },
    changeShift(index) {
        this.medicData.onDuty = index;
        var me = this.medicDutyList.find(
            (item) => item.medicId == this.medicData.medicId
        );
        console.log('me + ' + me + ' ' + JSON.stringify(this.medicData))
        if (me) {
            me.onDuty = this.medicData.onDuty;
            axios.post('https://eclipse_EmsCad/ChangeShift', {
                    shift: index,
                    medicId: this.medicData.medicId
            }).then(() => { return; }).catch(function () { console.log() }); 
        }
        
      // Смена статуса игрока "на смене"
    },
    panicClick() {
        this.panicOn = !this.panicOn;

        this.medicData.onPanic = this.panicOn;
        var me = this.medicDutyList.find(
            (item) => item.medicId == this.medicData.medicId
        );
        me.onPanic = this.medicData.onPanic;
        this.onPanic();
        // Нажата кнопка паники
        if(this.panicOn == true) {
            axios.post('https://eclipse_EmsCad/PanicOn', {

            }).then(() => { return; }).catch(function () { console.log() }); 
        }
        else {
            axios.post('https://eclipse_EmsCad/PanicOff', {
                shift: this.medicData.onDuty,
                medicId: this.medicData.medicId
            }).then(() => { return; }).catch(function () { console.log() }); 
        }
    },
    convertPoliceCodes() {
      this.policeCodes = {};
      police10codes.forEach((element, index) => {
        this.policeCodes[index] = { text: element.toString(), show: true };
      });
    },
    checkPaid(bool) {
      if (bool) {
        return "Yes";
      } else {
        return "No";
      }
    },
    checkLicense(bool) {
      if (bool) {
        return "Присутствует";
      } else {
        return "Отсутствует";
      }
    },
    addTicketToActivePlayer() {
      this.showModalA = true;
    },
    addInsuranceToPlayer() {
        this.$set(this.activePlayer, "insurance", true);
        axios
          .post("https://eclipse_EmsCad/AddInsurance", {
            id: this.activePlayer.phone
          })
          .then(() => {
            return;
          })
          .catch(function() {
            console.log();
          });
    },
    removeInsuranceFromPlayer() {
        this.$set(this.activePlayer, "insurance", false);
        axios
          .post("https://eclipse_EmsCad/RemoveInsurance", {
            id: this.activePlayer.phone
          })
          .then(() => {
            return;
          })
          .catch(function() {
            console.log();
          });
    },
    deleteTicket(index) {
        var id = this.activeTickets[index].id
        this.activeTickets.splice(index, 1);
        axios
          .post("https://eclipse_EmsCad/DeleteTicket", {
            id: id
          })
          .then(() => {
            return;
          })
          .catch(function() {
            console.log();
          });
        // Удаляем штраф
    },
    addViolationToList(index) {
        this.activeViolations.push(this.allLaws[index]);
        this.calculateViolations();
    },
    deleteViolationFromList(index) {
        this.activeViolations.splice(index, 1);
        this.calculateViolations();

    
    },
    clearViolationList() {
        this.activeViolations = [];
        this.calculateViolations();
    },
    calculateViolations(){
        this.violationsSum = 0;
        this.violationsTime = 0;
        this.activeViolations.forEach(item=> {
            if (item.price) {
                this.violationsSum += parseInt(item.price);
            }
            if (item.penalty) {
                this.violationsTime += parseInt(item.penalty);
            }
        });
        this.violationsTimeConverted = this.violationsTime / 60;
    },
    issueFine() {
      this.activeViolations.forEach((item, index) => {
        var newitem = item;
        newitem.id = 0;
        newitem.ownerId = this.activePlayer.id;
        newitem.operatorId = this.playerData.id;
        newitem.date = new Date();
        newitem.paid = false;
        newitem.text = this.activeViolations[index].name;
        // this.activeTickets.push(newitem);
        // Выписываем штраф

        axios
          .post("https://eclipse_EmsCad/NewTicket", {
            code: newitem.code,
            date: newitem.date,
            fulltext: newitem.fulltext,
            name: newitem.name,
            operatorId: newitem.operatorId,
            ownerId: newitem.ownerId,
            paid: newitem.paid,
            show: true,
            text: newitem.text,
            price: newitem.price,
            penalty: newitem.penalty
          })
          .then(() => {
            return;
          })
          .catch(function() {
            console.log();
          });
          
      });
      this.addTicketsToPlayers();

      this.showModalA = false;
      this.clearViolationList();
    },
    getRandomInt(min, max) {
      min = Math.ceil(min);
      max = Math.floor(max);
      return Math.floor(Math.random() * (max - min)) + min;
    },
    showWantedPerson(status) {
      return this.playersList.filter((i) => i.status === status);
    },
    showPatients(){
        return this.playersList.filter((i) => i.status != 0 & i.status != 1);
    },
    showWantedVehicles(status) {
      return this.vehiclesList.filter((i) => i.status === status);
    },
    showOnDuty(status) {
      if (status) {
        return this.medicDutyList.filter((i) => i.onDuty != 0);
      } else {
        return this.medicDutyList.filter((i) => i.onDuty == 0);
      }
    },
    groupBy(array, key) {
      return array.reduce(function(rv, x) {
        (rv[x[key]] = rv[x[key]] || []).push(x);
        return rv;
      }, {});
    },
    showAdamList() {
      this.medicAdamList = [];
      var adamList = [];
      this.medicDutyList.forEach((item) => {
        if (item.adam != undefined) {
          adamList.push(item);
        }
      });
      this.medicAdamList = this.groupBy(adamList, "adam");
    },
    generateAdamName(index) {
      var elem = this.medicAdamList[index];
      if (elem) {
        var array = [];
        var array2 = [];
        elem.forEach((item) => array.push(item.callsign.slice(0, 1)));
        var LetterA = Math.min.apply(Math, array);
        elem.forEach((item) => array2.push(item.rank));
        var LetterB = Math.max.apply(Math, array2);
        var rank = elem.find((cop) => cop.rank == LetterB);
        return LetterA + "A—" + rank.callsign.slice(-1);
      } 
      else {
        return (
          this.medicData.callsign.slice(0, 1) +
          "A—" +
          this.medicData.callsign.slice(-1))
      }
    },
        showPoliceFullPage(index) {
            this.activeCop = this.medicDutyList[index];
            this.showModalB = true;
            this.callsignTemp = this.activeCop.callsign;
        },
        saveNewCallsign() {
            this.activeCop.callsign = this.callsignTemp;
            axios.post('https://eclipse_EmsCad/SaveNewCallsign', {
                callsign: this.activeCop.callsign,
                medicId: this.activeCop.medicId
            }).then(() => { return; }).catch(function () { console.log() }); 
        },
        fireCop(player) {
            var index = this.medicDutyList.findIndex(
                (item) => item.medicId == player.medicId
            );
            this.medicDutyList.splice(index, 1);
            this.showModalB = false;
            this.sureFire = false;
            this.showDropdownD = false;
            this.showAdamList();
            axios.post('https://eclipse_EmsCad/FireCop', {
                medicId: player.medicId
            }).then(() => { return; }).catch(function () { console.log() }); 
        },
        changePlayerRank(index) {
            this.activeCop.rank = index;
            this.showDropdownD = false;

            axios.post('https://eclipse_EmsCad/ChangePlayerRank', {
                rank: this.activeCop.rank,
                medicId: this.activeCop.medicId
            }).then(() => { return; }).catch(function () { console.log() }); 
        },
        onPanic() {
            var panic = false;
            this.medicDutyList.forEach(element => {
                if (element.onPanic == true) {
                    panic = true;
                }
            });
            return panic;
        },
        addImageToPlayer() {
            this.activePlayer.picture = this.inputImage;
            this.inputImage = undefined;
            this.showModalC = false;
            axios.post('https://eclipse_EmsCad/AddImageToPlayer', {
                image: this.activePlayer.picture,
                id: this.activePlayer.phone
            }).then(() => { return; }).catch(function () { console.log() }); 
        },
        convertCrew(){
            this.medicCallsList.forEach(element => {
                var arr = JSON.parse(element.crew)
                element.crew = arr
            });
        },
        doCommand(e) {
            if(e.keyCode == 27) {
                    axios.post('https://eclipse_EmsCad/CloseCad', {
                    }).then(() => { return; }).catch(function () { console.log() });  
            }
            else if(e.keyCode == 192) {
                    axios.post('https://eclipse_EmsCad/Tilda', {
                    }).then(() => { return; }).catch(function () { console.log() });  
            }
        },
    },
    mounted() {
        // TEST DATA

        // this.playersList = [
        //     {"id": 1,"isActive":true,"balance":"0","age":"06/09/1998","name":"Player 1","gender":"male","phone":"960-3549","picture":"https://i.imgur.com/6odiNea.png","status":1,"registered":"06/09/1998"},
        //     {"id": 2,"isActive":true,"balance":"0","age":"05/09/1998","name":"Player 2","gender":"male","phone":"555-0000","picture":null,"status":1,"registered":"05/09/1998"}
        // ]

        // this.medicDutyList = [
        //     {name: "Медик 1", medicId: 1, rank: 3, callsign: "M-1", onDuty: 0, onPanic: false,},
        //     {name: "Медик 1", medicId: 2, rank: 1, callsign: "M-2", onDuty: 1, onPanic: false,},
        //     {name: "Медик 2", medicId: 3, rank: 2, callsign: "M-3", onDuty: 2, onPanic: false,},
        //     {name: "Медик 3", medicId: 4, rank: 0, callsign: "M-4", onDuty: 3, onPanic: false,},
        //     {name: "Javier Bennett", id: 1, medicId: 444, rank: 0, callsign: "M-4", onDuty: 1, onPanic: false,},
        // ]

        // this.medicCallsList = [
        //     {id: 11, status: 3, date: "Tue Jan 14 1993 11:51:18 GMT+0300", crew: [1,2], message: "Помогите! Мне плохо, я умираю!", from: 2},
            
        // ]

        // this.ticketsList = [
        //     {id: 0, operatorId: 444, ownerId: 1, date: "Sat Aug 14 2021 17:40:50 GMT+0300", text: "123 Проверка", price: "100", paid: "true"}
        // ]

        this.showAll(this.medicCallsList);
        this.showAll(this.playersList);
        this.showAll(this.allLaws);
        this.showAll(this.ticketsList);

        // END TEST DATA

        this.show = true;
        if (this.playerData.roles == "ambulance") {
            this.medicData = this.playerData.medicData;
            console.log(JSON.stringify(this.medicData))
        } else {
            this.medicData = { rank: "Not found", callsing: "Not found" };
        }
        window.addEventListener(
            "message",
            (event) => {
                if (event.data.type == "VehicleData") {
                    console.log('VehicleData' + event.data.data)
                    this.vehiclesList = JSON.parse(event.data.data);
                    this.showAll(this.vehiclesList)
                } else if (event.data.type == "UserData") {
                    console.log('UserData: ' + event.data.data);
                    this.playersList = JSON.parse(event.data.data);
                    this.showAll(this.playersList);
                    this.addTicketsToPlayers();
                    this.addVehiclesToPlayers();
                } else if (event.data.type == "VehiclesStatus") {
                    console.log('VehiclesStatus' + event.data.data)
                    var statusList = JSON.parse(event.data.data);
                    statusList.forEach((item) => {
                        var car = this.vehiclesList.findIndex(
                            (elem) => elem.number == item.number
                        );
                        this.vehiclesList[car].status = item.status;
                    });
                    this.showAll(this.vehiclesList);
                } else if (event.data.type == "GetTickets") {
                    console.log('GetTickets' + event.data.data)
                    this.ticketsList = JSON.parse(event.data.data);
                    this.showAll(this.ticketsList);
                    this.addTicketsToPlayers();
                    this.activeTickets = this.activePlayer.tickets;
                } else if (event.data.type == "CitizensStatus") {
                    console.log('CitizensStatus + ' + event.data.data)
                    var statusUsersList = JSON.parse(event.data.data);
                    statusUsersList.forEach((item) => {
                        var car = this.playersList.findIndex(
                            (elem) => elem.name == item.name
                        );
                        this.playersList[car].status = item.status;
                    });
                    //this.showAll(this.playersList);
                } else if (event.data.type == "GetAmbulanceCalls") {
                    console.log('GetAmbulanceCalls + ' + event.data.data)
                    var list = JSON.parse(event.data.data);
                    console.log(list[0].crew)
                    list.forEach(item => {
                        item.crew = JSON.parse(item.crew);
                    })
                    this.medicCallsList = list;
                    this.showAll(this.medicCallsList);
                    console.log(this.medicCallsList)
                } else if (event.data.type == "GetAmbulanceOnDuty") {
                    console.log('GetAmbulanceOnDuty' + event.data.data)
                    this.medicDutyList = JSON.parse(event.data.data);
                    console.log(this.medicDutyList)
                    this.onPanic();
                    this.showAdamList();

                    this.sortyBycallsign(this.medicDutyList);

                    this.addTicketsToPlayers();
                    this.addVehiclesToPlayers();

                    this.showAll(this.playersList);
                    this.showAll(this.medicCallsList);
                    this.showAll(this.vehiclesList);
                    this.showAll(this.allLaws);

                    this.sortByDate(this.medicCallsList);
                    this.sortByNames(this.playersList);
                    this.sortByNumbers(this.vehiclesList);

                    this.convertPoliceCodes();
                }
            },

            false
        );
    },
    created() {
      window.addEventListener('keydown', this.doCommand);
    },
    destroyed() {
        window.addEventListener('keydown', this.doCommand);
    }
}
</script>

<style scoped>
.loading {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%) scale(7);
}
.loading svg polyline {
    fill: none;
    stroke-width: 1;
    stroke-linecap: round;
    stroke-linejoin: round;
}
.loading svg polyline#back {
    stroke: var(--medic)
}
.loading svg polyline#front {
    stroke: var(--white);
    stroke-dasharray: 12, 36;
    stroke-dashoffset: 48;
    animation: dash 1s linear infinite;
}
@-moz-keyframes dash {
    62.5% {
        opacity: 0;
    }
    to {
        stroke-dashoffset: 0;
    }
}
@-webkit-keyframes dash {
    62.5% {
        opacity: 0;
    }
    to {
        stroke-dashoffset: 0;
    }
}
@-o-keyframes dash {
    62.5% {
        opacity: 0;
    }
    to {
        stroke-dashoffset: 0;
    }
}
@keyframes dash {
    62.5% {
        opacity: 0;
    }
    to {
        stroke-dashoffset: 0;
    }
}
</style>
