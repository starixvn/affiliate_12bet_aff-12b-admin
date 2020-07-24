import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText, globalResource } from '../libs/constants';
import { setPagingOption, fetchData, setCheckedLanguages, setShouldResetScrollPosition, setParam } from '../actions';
import SelectAll from '../../../common/components/SelectAll';

export class General extends PureComponent {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.props.fetchData(this.props.param);
    }

    onCheckItem = (isChecked, languageId) => {
        const newCheckedLanguages = Object.assign({}, this.props.checkedLanguages, { [languageId]: isChecked });

        this.props.setShouldResetScrollPosition(false);
        this.props.setCheckedLanguages(newCheckedLanguages);
    }

    handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };
        const newParam = Object.assign({}, this.props.param, newPaging);

        this.props.setPagingOption(newPaging);
        this.props.setParam(newParam);
    };

    _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={50}></Cell>
                <Cell header={true} colWidth={50} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={500} title={headerText.Language}>{headerText.Language}</Cell>
                <Cell header={true} colWidth={50} title={headerText.View}>{headerText.View}</Cell>
                <Cell header={true} colWidth={50} title={headerText.Edit}>{headerText.Edit}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { pagingOption } = this.props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, i + startIndex));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="7">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center">
                    <input type="checkbox" checked={this.props.checkedLanguages[dataItem.LanguageId]}
                        onChange={(e) => this.onCheckItem(e.target.checked, dataItem.LanguageId)} />
                </Cell>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={dataItem.LanguageName}>{dataItem.LanguageName}</Cell>
                <Cell className="text-center" title={headerText.View}>
                    <Button color="link p-0" onClick={() => this._handleEdit(dataItem.LanguageId)}>
                        <span className="oi oi-pencil text-secondary"></span>
                    </Button>
                </Cell>
                <Cell className="text-center" title={headerText.Edit}>
                    <Button color="link p-0" onClick={() => this._handleEdit(dataItem.LanguageId)}>
                        <span className="oi oi-x text-danger"></span>
                    </Button>
                </Cell>
            </Row>
        )
    }

    render() {
        const { gridData } = this.props;

        return gridData && (
            <div className="data-content">
                <SelectAll
                    checkedList={this.props.checkedLanguages}
                    setCheckedList={this.props.setCheckedLanguages}
                    setShouldResetScrollPosition={this.props.setShouldResetScrollPosition}
                />
                <div className="table-responsive table-striped my-2">
                    <Table
                        minWidth={1000}
                        autoWidth={true}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(gridData)}
                        containerPadding={20}
                        isPaging={true}
                        pageOption={this.props.pagingOption}
                        onPaging={this.handlePaging}
                        shouldResetScrollPosition={this.props.shouldResetScrollPosition}
                    />
                </div>
            </div>
        );
    }
}


function mapStateToProps({ pagingOption, param, checkedLanguages, shouldResetScrollPosition, gridData }) {
    return Object.assign({}, { pagingOption, param, checkedLanguages, shouldResetScrollPosition, gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, setPagingOption, setCheckedLanguages, setShouldResetScrollPosition, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Language);