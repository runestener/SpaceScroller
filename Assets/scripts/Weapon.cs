using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour {

	public float cooldownTime;
	public float damage;
	public float projectileSpeed;


	protected bool _canShoot = true;
	protected Transform _shootTransform;

	public Projectile projectileObject;
	protected List<Projectile> _projectiles = new List<Projectile>();
	protected List<Projectile> projectiles {
		get {
			if (this._projectiles == null){
				this._projectiles = new List<Projectile>();
			}
			return this._projectiles;			 
		}
		set {
			if (this._projectiles == null) {
				this._projectiles = new List<Projectile>();
			}
			this._projectiles = value;
		}
	}

	public void SetupWeapon() {
		SetupWeapon (this.transform);
	}

	public void SetupWeapon(Transform shootPosition) {
		this._canShoot = true;
		this._shootTransform = shootPosition;

		for (int i = 0; i < 10; i++) {
			Projectile p = Instantiate(projectileObject) as Projectile;
			p.gameObject.name = string.Format("projectile_{0}", i);
			projectiles.Add(p );
			p.InstantiateProjectil(this.damage);
		}
	}

	public void Fire() {
		if (this._canShoot) {
			this._canShoot = false;
			StartCoroutine( WeaponCooldown() );
			Projectile projectile = projectiles.Find(p => p.isEnabled == false);
			projectile.InitiateProjectile(this._shootTransform.position);
			//Debug.Log ("firing weapon");
		}
	}

	protected IEnumerator WeaponCooldown() {
		float t = Time.time + cooldownTime;
		while (t >= Time.time) {
			yield return null;
		}
		this._canShoot = true;
		yield return null;
	}
}
