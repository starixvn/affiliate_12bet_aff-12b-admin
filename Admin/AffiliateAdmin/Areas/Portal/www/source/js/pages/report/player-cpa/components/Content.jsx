import React from 'react'
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { toggleSubmit } from '../actions';
import { toggleLoader } from '../../../../actions/loaderAction';
import ReportTitle from '../../../../components/ReportTitle';
import GridData from './GridData';
import ExportExcel from '../libs/exportExcel';
import { getPlayerCpaData } from '../libs/service';
import { headerRows } from '../libs/constants';

const Title = ({ reportTitle, params, toggleLoader}) => {

    const handleExportExcel = async () => {
        toggleLoader(true);
        try{
            const data = await getPlayerCpaData(params, true);
            const exportExcel = new ExportExcel(data, reportTitle, headerRows);
            exportExcel.export();
        }
        catch(err){
            console.log('err', err);
        }
        finally{
            toggleLoader(false);
        }
    }

    return (
        <div className="d-flex align-items-center">
            {reportTitle}
            <button type="button" className="ml-auto btn btn-link btn-excel" onClick={handleExportExcel} title="Export Excel">
                <span className="oi icon-excel"></span>
            </button>
        </div>
    )
}

export const Content = ({ reportTitle, toggleLoader, params }) => {
    return (
        <div className='data-content my-2 report-content-container'>
            {
                reportTitle && (
                    <ReportTitle content={<Title reportTitle={reportTitle} params={params} toggleLoader={toggleLoader} />} />
                )
            }
            <GridData />
        </div>
    )
}

const mapStateToProps = (state) => {
    return {
        isSubmit: state.isSubmit,
        params: state.params,
        reportTitle: state.reportTitle
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({ toggleSubmit, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);