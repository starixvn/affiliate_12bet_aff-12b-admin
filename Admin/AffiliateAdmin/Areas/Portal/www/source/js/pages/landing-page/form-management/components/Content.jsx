import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { globalResource } from '../../../../modules/globalResource';
import { headerText, maxWidthHeight } from '../libs/constants';
import { setSelectedFormId, setPagingOption, fetchData, setCheckedForms, setShouldResetScrollPosition, setParam } from '../actions';

export const Content = (props) => {

    useEffect(() => {
        const { param } = props;

        param && props.fetchData(param);
    }, [props.param]);

    const _onCheckItem = (isChecked, formId) => {
        const newCheckedForms = Object.assign({}, props.checkedForms, { [formId]: isChecked });

        props.setShouldResetScrollPosition(false);
        props.setCheckedForms(newCheckedForms);
    }

    const _onDeleteItem = (formId) => {
        // Handle single item deletion
        if (window.confirm('Are you sure you want to delete this item?')) {
            // Add delete logic here - this would typically call a delete action
            console.log('Delete item with ID:', formId);
            // props.deleteItem(formId); // Uncomment when delete action is available
        }
    }

    const _handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };
        const newParam = Object.assign({}, props.param, newPaging);

        props.setPagingOption(newPaging);
        props.setParam(newParam);
    };

    function _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={20} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={200} title={headerText.FormName}>{headerText.FormName}</Cell>
                <Cell header={true} colWidth={200} title={headerText.PreviewImageName}>{headerText.PreviewImageName}</Cell>
                <Cell header={true} colWidth={150} title={headerText.Demensions}>{headerText.Demensions}</Cell>
                <Cell header={true} colWidth={120} title={headerText.PreviewImage}>{headerText.PreviewImage}</Cell>
                <Cell header={true} colWidth={30} title={headerText.Action}>{headerText.Action}</Cell>
            </Row>
        );
    }

    function _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { pagingOption } = props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(_buildDataRow(dataItem, i + startIndex));
            }
        }
        else {
            dataRows.push(<Row key="no-info">
                <Cell className="text-center" colSpan="8">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    function _buildDataRow(dataItem, index) {
        const formId = dataItem.FormId;
        const formName = dataItem.FormName;
        const previewImageName = dataItem.PreviewImageName;
        const demensions = dataItem.Demensions;
        const previewImage = dataItem.PreviewImage;

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center">
                    <input type="checkbox" checked={props.checkedForms[formId] || false}
                        onChange={(e) => _onCheckItem(e.target.checked, formId)} />
                </Cell>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-center text-break" title={formName}>{formName}</Cell>
                <Cell className="text-center text-break" title={previewImageName}>{previewImageName}</Cell>
                <Cell className="text-center text-break" title={demensions}>{demensions}</Cell>
                <Cell className="text-center text-break" title={previewImage}>{previewImage}</Cell>
                <Cell className="text-center">
                    <img id={`img${formId}`} src={previewImage} alt="Preview Image" />
                </Cell>
                <Cell className="text-center">
                    <Button color="link" onClick={() => props.setSelectedFormId(formId)} title="Edit">
                        <span className="oi oi-pencil text-secondary"></span>
                    </Button>
                    <Button color="link" onClick={() => _onDeleteItem(formId)} title="Delete" className="ml-1">
                        <span className="oi oi-trash text-danger"></span>
                    </Button>
                </Cell>
            </Row>
        )
    }

    return (
        <div className="data-content">
            <div className="banner-list-content table-responsive table-striped my-2">
                <Table
                    minWidth={1190}
                    autoWidth={true}
                    bodyHeight={300}
                    header={_renderHeader()}
                    body={_renderDataSection(props.gridData)}
                    containerPadding={20}
                    isPaging={true}
                    pageOption={props.pagingOption}
                    onPaging={_handlePaging}
                    shouldResetScrollPosition={props.shouldResetScrollPosition}
                />
            </div>
        </div>
    );
}

function mapStateToProps({ pagingOption, checkedForms, shouldResetScrollPosition, gridData }) {
    return Object.assign({}, { pagingOption, checkedForms, shouldResetScrollPosition, gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedFormId, fetchData, setPagingOption, setCheckedForms, setShouldResetScrollPosition }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Content));