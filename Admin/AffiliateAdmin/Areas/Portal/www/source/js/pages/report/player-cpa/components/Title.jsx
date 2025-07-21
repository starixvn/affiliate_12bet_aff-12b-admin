import React from 'react'
import { connect } from 'react-redux';

export const Title = ({ params }) => {
    const { fromDate, toDate, dealName, viewBy } = params
    const title = `PLAYER CPA REPORT - Deal Name contains '${dealName}' - View By ${viewBy} - FROM ${fromDate} TO ${toDate}`
    
    return (
        <div class="report-title-container">
            <div class="report-title">
                <div class="d-flex align-items-center">
                    {title}
                    <button type="button" class="ml-auto btn btn-link btn-excel" title="Export Excel">
                        <span class="oi icon-excel"></span>
                    </button>
                </div>
            </div>
        </div>
    )
}

const mapStateToProps = (state) => {
    return {
        params: state.params
    }
}

export default connect(mapStateToProps)(Title);
