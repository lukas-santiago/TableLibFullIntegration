import React from 'react'

export interface TableOptions {
  columns: any[]
  data: any[]
}

export function TableComponent(props: TableOptions) {
  const pagination = {
    limit: 10,
    prev: 0,
    next: 1,
  }

  const getHeader = () => {
    return props.columns.map((column, index) => {
      return (
        <th key={`getHeader-${column.name}-${index}`}>
          {column.name}
        </th>
      )
    })
  }
  const getData = () => {
    return props.data
      .filter(paginationFilter)
      .map((data, index) => (
        <tr key={`tbody-${index}`}>
          {props.columns.map((column, index1) => {
            let dataColumn = data[column.id]
            dataColumn = dataColumn ? dataColumn : null
            return (
              <th key={`${column.name}-${index1}-${index}`}>
                {dataColumn}
              </th>
            )
          })}
        </tr>
      ))
  }

  const paginationFilter = (data: any[], index: number) => {
    return index < pagination.limit
  }
  return (
    <div className="table-wrapper">
      <table>
        <thead>
          <tr>{getHeader()}</tr>
        </thead>
        <tbody>{getData()}</tbody>
      </table>
      <TableFooter />
    </div>
  )
}

function TableFooter(props: any) {
  return (
    <div className="footer">
      <div className="pagination">
        <button>Primeiro</button>
        <button>1</button>
        <button>Último</button>
      </div>
    </div>
  )
}
