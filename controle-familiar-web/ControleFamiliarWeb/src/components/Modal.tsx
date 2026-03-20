type Props = {
  open: boolean
  title: string
  children: React.ReactNode
  onClose: () => void
}

export default function Modal({ open, title, children, onClose }: Props) {

  if (!open) return null

  return (

    <div className="modal-overlay">

      <div className="modal">

        <div className="modal-header">

          <h3>{title}</h3>

          <button className="modal-close" onClick={onClose}>
            ✕
          </button>

        </div>

        <div className="modal-body">

          {children}

        </div>

      </div>

    </div>

  )

}