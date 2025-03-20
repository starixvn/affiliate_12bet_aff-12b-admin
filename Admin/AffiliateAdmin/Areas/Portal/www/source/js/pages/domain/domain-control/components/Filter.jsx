import React, { useState } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import MultipleSelect from '../../../../components/multiple-select';

import { setSelectedMarketId, fetchData, setAffSubDomainList, setShouldResetScrollPosition } from '../actions';
import { headerText, globalResource } from '../libs/constants';

export const Filter = (props) => {
    const { marketList, affSubDomainList } = props;
    const [market, setMarket] = useState('');

    const _handleSearch = () => {
        props.setSelectedMarketId(market);
        props.fetchData(market, affSubDomainList);
    }

    const handleChangeSubDomain = (item, selectedSubDomains) => {
        props.setShouldResetScrollPosition(false);

        const selectedSubDomainList = selectedSubDomains.split(',').filter(p => !!p);

        const newSubDomainList = affSubDomainList.map(item => {
            const { columnKey, columnName } = item;
            const checked = selectedSubDomainList.indexOf(columnKey) !== -1;

            return {
                columnKey,
                columnName,
                checked
            };
        })

        props.setAffSubDomainList(newSubDomainList);
    }

    return marketList && (
        <div className="filter-container my-2">
            <Form inline tag="div">
                <FormGroup className="mr-sm-2">
                    <Label className="mr-sm-2">{headerText.Market}</Label>
                    <Input type="select" name="market" value={market} onChange={(e) => setMarket(e.target.value)}>
                        <option key={'market-all'} value={''}>{globalResource.All}</option>
                        {
                            marketList.map(item => {
                                return (<option key={`market-${item.MarketId}`} value={item.MarketId}>{item.MarketName}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <FormGroup className="mr-sm-3">
                    <Label className="mr-sm-2">{headerText.AffSubDomain}</Label>
                    <MultipleSelect
                        dataSource={affSubDomainList}
                        keyField="columnKey"
                        valueField="columnName"
                        statusField="checked"
                        maxDisplayItemCount={1}
                        noneSelectedLabel="Please Select"
                        selectedAllLabel="All"
                        onChange={handleChangeSubDomain} />
                </FormGroup>
                <Button outline color="primary" type="button" onClick={_handleSearch}>
                    <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                </Button>
            </Form>
        </div>
    )
}

function mapStateToProps({ marketList, affSubDomainList }) {
    return Object.assign({}, { marketList, affSubDomainList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedMarketId, fetchData, setAffSubDomainList, setShouldResetScrollPosition }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Filter));