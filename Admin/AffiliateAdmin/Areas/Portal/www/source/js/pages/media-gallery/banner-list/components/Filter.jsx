import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';

import { setParam, setParamFromHome, getSizeList } from '../actions';
import { headerText, globalResource, Asian, selectedAllProduct } from '../libs/constants';
import { handleAfterChangeFromDate, handleAfterChangeToDate } from '../../../../modules/utils';

const minDate = '7/1/2022';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        const { today } = this.props;
        const now = new Date(today);

        this.nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;

        this.state = {
            targetProductId: selectedAllProduct,
            language: '',
            affType: Asian,
            fromDate: `${now.getMonth() + 1}/${1}/${now.getFullYear()}`,
            toDate: this.nowString,
            size: ''
        }
    }

    componentDidMount() {
        const { paramFromHome } = this.props;

        if (paramFromHome && paramFromHome.IsFromHome) {
            this._handleSearch();
            this.props.setParamFromHome({ IsFromHome: false });
        }
    }

    _handleChange = (event) => {
        const name = event.target.name;
        const value = event.target.value;

        const param = Object.assign({}, this.state, { [name]: value });

        this.props.getSizeList(param);
        this.setState({ [name]: value });
    }

    _handleChangeFromDate = (momentDate) => {
        this.setState(prevState => handleAfterChangeFromDate(momentDate, prevState.toDate));
    }

    _handleChangeToDate = (momentDate) => {
        this.setState(prevState => handleAfterChangeToDate(momentDate, prevState.fromDate));
    }

    _handleSearch = () => {
        const { pagingOption } = this.props;

        const newParam = Object.assign({}, this.state, { PageIndex: 1, PageSize: pagingOption.PageSize });

        this.props.setParam(newParam);
    }

    render() {
        const { targetProductList, languageList, sizeList, affTypeList } = this.props;
        const { targetProductId, language, affType, size, fromDate, toDate } = this.state;

        return (
            <div className="filter-container my-2 pb-0">
                <Form inline tag="div">
                    <FormGroup className="mr-sm-3 pb-2">
                        <Label for="targetProductId" className="mr-sm-2">{headerText.TargetProduct}</Label>
                        <Input type="select" name="targetProductId" id="targetProductId" value={targetProductId} onChange={this._handleChange}>
                            <option key={'targetProduct-all'} value={''}>{globalResource.All}</option>
                            {
                                targetProductList && targetProductList.map(item => {
                                    return (<option key={`targetProduct-${item.name}`} value={item.key}>{item.name}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup className="mr-sm-3 pb-2">
                        <Label for="language" className="mr-sm-2">{headerText.Language}</Label>
                        <Input type="select" name="language" id="language" value={language} onChange={this._handleChange}>
                            <option key={'language-all'} value={''}>{globalResource.All}</option>
                            {
                                languageList && languageList.map(item => {
                                    return (<option key={`language-${item.LanguageName}`} value={item.LanguageName}>{item.LanguageName}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    {
                        affTypeList && (
                            <FormGroup className="mr-sm-3 pb-2">
                                <Label for="affType" className="mr-sm-2">{headerText.AffType}</Label>
                                <Input type="select" name="affType" id="affType" value={affType} onChange={this._handleChange}>
                                    {
                                        affTypeList.map(item => {
                                            return (<option key={`affType-${item.key}`} value={item.key}>{item.name}</option>)
                                        })
                                    }
                                </Input>
                            </FormGroup>
                        )
                    }
                    <FormGroup className="mr-sm-3 pb-2">
                        <Label for="size" className="mr-sm-2">{headerText.Size}</Label>
                        <Input type="select" name="size" id="size" value={size} onChange={(e) => this.setState({ size: e.target.value })}>
                            <option key={'size-all'} value={''}>{globalResource.All}</option>
                            {
                                sizeList && sizeList.map(item => {
                                    return (<option key={`size-${item.name}`} value={item.name}>{item.name}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup className="mr-sm-2 pb-2">
                        <Label className="mr-sm-2">{headerText.CopiedFrom}</Label>
                        <DatePicker minDate={minDate} maxDate={this.nowString} date={fromDate}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this._handleChangeFromDate} />
                    </FormGroup>
                    <FormGroup className="mr-sm-2 pb-2">
                        <Label className="mr-sm-2">{globalResource.To}</Label>
                        <DatePicker minDate={fromDate} maxDate={this.nowString} date={toDate}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this._handleChangeToDate} />
                    </FormGroup>
                    <Button outline color="primary" type="button" className="mb-2" onClick={this._handleSearch}>
                        <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                    </Button>
                </Form>
            </div>
        );
    }
}

function mapStateToProps({ targetProductList, languageList, sizeList, affTypeList, pagingOption, paramFromHome, today }) {

    return Object.assign({}, {
        targetProductList, languageList, sizeList, affTypeList, pagingOption, paramFromHome, today
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam, setParamFromHome, getSizeList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);